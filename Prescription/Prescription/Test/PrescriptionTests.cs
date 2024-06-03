using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Prescription.Controllers;
using Prescription.DTOs;
using Prescription.Models;
using Prescription.Repositories;
using Prescription.Services;

namespace Prescription.Test;

public class PrescriptionTests
{
    private PrescriptionController _prescriptionController;
    private PrescriptionService _prescriptionService;
    private DoctorService _doctorService;
    private MedicamentService _medicamentService;
    private PatientService _patientService;

    private Mock<IDoctorRepository> _doctorRepositoryMock;
    private Mock<IMedicamentRepository> _medicamentRepositoryMock;
    private Mock<IPatientRepository> _patientRepositoryMock;
    private Mock<IPrescriptionRepository> _prescriptionRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    [SetUp]
    public void Setup()
    {
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorRepositoryMock
            .Setup(repo => repo.GetDoctor(It.Is<int>(idDoctor => idDoctor == 0), It.IsAny<CancellationToken>()))
            .Throws(new InvalidOperationException("Doctor id 0 not found"));
        _doctorRepositoryMock
            .Setup(repo => repo.GetDoctor(It.Is<int>(idDoctor => idDoctor != 0), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int idDoctor, CancellationToken _) => new Doctor
            {
                IdDoctor = idDoctor
            });

        _medicamentRepositoryMock = new Mock<IMedicamentRepository>();
        _medicamentRepositoryMock
            .Setup(repo => repo.GetMedicament(It.Is<string>(name => name == "NonExistentMedicament"), It.IsAny<CancellationToken>()))
            .Throws(new InvalidOperationException("Medicament NonExistentMedicament not found"));
        _medicamentRepositoryMock
            .Setup(repo => repo.GetMedicament(It.Is<string>(name => name != "NonExistentMedicament"), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string name, CancellationToken _) => new Medicament
            {
                IdMedicament = 1,
                Name = name,
            });

        _patientRepositoryMock = new Mock<IPatientRepository>();
        _patientRepositoryMock
            .Setup(repo => repo.GetOrAddPatient(It.IsAny<PatientData>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PatientData patientData, CancellationToken _) => new Patient(patientData)
            {
                IdPatient = 1
            });
        _patientRepositoryMock
            .Setup(repo => repo.GetPatient(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int idPatient, CancellationToken _) => new Patient
            {
                IdPatient = idPatient
            });

        _prescriptionRepositoryMock = new Mock<IPrescriptionRepository>();
        _prescriptionRepositoryMock
            .Setup(repo => repo.AddPrescription(It.IsAny<Models.Prescription>(), It.IsAny<CancellationToken>()))
            .Callback((Models.Prescription prescription, CancellationToken _) => prescription.IdPrescription = 1);
        _prescriptionRepositoryMock
            .Setup(repo =>
                repo.AddPrescriptionMedicament(It.IsAny<PrescriptionMedicament>(), It.IsAny<CancellationToken>()));

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()));
        _unitOfWorkMock
            .Setup(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()));
        _unitOfWorkMock
            .Setup(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()));
        _unitOfWorkMock
            .Setup(x => x.DisposeAsync());
 
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
        _medicamentService = new MedicamentService(_medicamentRepositoryMock.Object);
        _patientService = new PatientService(_patientRepositoryMock.Object);
        _prescriptionService = new PrescriptionService(
            _prescriptionRepositoryMock.Object,
            _doctorService,
            _medicamentService,
            _patientService,
            _unitOfWorkMock.Object
        );
        _prescriptionController = new PrescriptionController(_prescriptionService);
    }

    [Test]
    public async Task AddPrescription_NonExistentDoctor_ReturnsBadRequest()
    {
        //Arrange
        var prescribeDto = new PrescribeDto
        {
            PatientData = new PatientData
            {
                FirstName = "John",
                LastName = "Rambo",
                BirthDate = new DateTime(1960,1,1)
            },
            PrescriptionDetails = [
                new PrescriptionDetail
                {
                    MedicamentName = "Aspirin",
                    Dose = 1,
                    Details = "It will pass"
                }
            ],
            Date = new DateTime(2024,5,15),
            DueDate = new DateTime(2024,6,15)
        };

        //Act
        var result = await _prescriptionController.Prescribe(0, prescribeDto, CancellationToken.None);
        
        //Assert
        Assert.That(result is BadRequestObjectResult);
        Assert.That("Doctor id 0 not found", Is.EqualTo(((BadRequestObjectResult)result).Value));

    }

    [Test]
    public async Task AddPrescription_NonExistentMedicament_ReturnsBadRequest()
    {
        //Arrange
        var prescribeDto = new PrescribeDto
        {
            PatientData = new PatientData
            {
                FirstName = "John",
                LastName = "Rambo",
                BirthDate = new DateTime(1960,1,1)
            },
            PrescriptionDetails = [
                new PrescriptionDetail
                {
                    MedicamentName = "NonExistentMedicament",
                    Dose = 1,
                    Details = "It's placebo"
                }
            ],
            Date = new DateTime(2024,5,15),
            DueDate = new DateTime(2024,6,15)
        };

        //Act
        var result = await _prescriptionController.Prescribe(1, prescribeDto, CancellationToken.None);
            
        //Assert
        Assert.That(result is BadRequestObjectResult);
        Assert.That("Medicament NonExistentMedicament not found", Is.EqualTo(((BadRequestObjectResult)result).Value));

    }

    [Test]
    public async Task AddPrescription_TooManyMedicaments_ReturnsBadRequest()
    {
        //Arrange
        var prescribeDto = new PrescribeDto
        {
            PatientData = new PatientData
            {
                FirstName = "John",
                LastName = "Rambo",
                BirthDate = new DateTime(1960,1,1)
            },
            PrescriptionDetails = [],
            Date = new DateTime(2024,5,15),
            DueDate = new DateTime(2024,6,15)
        };

        var i = 0;
        while (i < 11)
        {
            prescribeDto.PrescriptionDetails.Add(new PrescriptionDetail
            {
                MedicamentName = "Aspirin",
                Dose = i++,
                Details = "Take one more son"
            });
        }

        //Act
        var result = await _prescriptionController.Prescribe(1, prescribeDto, CancellationToken.None);
            
        //Assert
        Assert.That(result is BadRequestObjectResult);
        Assert.That("Prescription cannot include more than 10 medicaments", Is.EqualTo(((BadRequestObjectResult)result).Value));
    }

    [Test]
    public async Task AddPrescription_DueDateBeforeDate_ReturnsBadRequest()
    {
        //Arrange
        var prescribeDto = new PrescribeDto
        {
            PatientData = new PatientData
            {
                FirstName = "John",
                LastName = "Rambo",
                BirthDate = new DateTime(1960,1,1)
            },
            PrescriptionDetails = [ 
                new PrescriptionDetail
            {
                MedicamentName = "Aspirin",
                Dose = 1,
                Details = "It will pass"
            }
            ],
            Date = new DateTime(2024,7,15),
            DueDate = new DateTime(2024,6,15)
        };

        //Act
        var result = await _prescriptionController.Prescribe(1, prescribeDto, CancellationToken.None);
            
        //Assert
        Assert.That(result is BadRequestObjectResult);
        Assert.That("Prescription date cannot be after its due date", Is.EqualTo(((BadRequestObjectResult)result).Value));

    }

    [Test]
    public async Task AddPrescription_ReturnsCreatedResult()
    {
        //Arrange
        var prescribeDto = new PrescribeDto
        {
            PatientData = new PatientData
            {
                FirstName = "John",
                LastName = "Rambo",
                BirthDate = new DateTime(1960,1,1)
            },
            PrescriptionDetails = [ 
                new PrescriptionDetail
                {
                    MedicamentName = "Aspirin",
                    Dose = 1,
                    Details = "It will pass"
                }
            ],
            Date = new DateTime(2024,5,15),
            DueDate = new DateTime(2024,6,15)
        };

        //Act
        var result = await _prescriptionController.Prescribe(1, prescribeDto, CancellationToken.None);
            
        //Assert
        Assert.That(result is CreatedResult);
    }
}