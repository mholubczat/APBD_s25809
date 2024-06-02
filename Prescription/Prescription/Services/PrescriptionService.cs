using Prescription.DTOs;
using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services;

public interface IPrescriptionService
{
    Task AddPrescription(int idDoctor, PrescribeDto dto, CancellationToken cancellationToken);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IDoctorService _doctorService;
    private readonly IMedicamentService _medicamentService;
    private readonly IPatientService _patientService;
    private readonly IUnitOfWork _unitOfWork;

    public PrescriptionService(
        IPrescriptionRepository prescriptionRepository,
        IDoctorService doctorService,
        IMedicamentService medicamentService, 
        IPatientService patientService,
        IUnitOfWork unitOfWork)
    {
        _prescriptionRepository = prescriptionRepository;
        _doctorService = doctorService;
        _medicamentService = medicamentService;
        _patientService = patientService;
        _unitOfWork = unitOfWork;
    }

    public async Task AddPrescription(int idDoctor, PrescribeDto dto, CancellationToken cancellationToken)
    {
        var doctor = await _doctorService.GetDoctor(idDoctor, cancellationToken);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var patient = await _patientService.GetOrAddPatient(dto.PatientData, cancellationToken);

            var prescription = new Models.Prescription
            {
                Date = dto.Date,
                DueDate = dto.DueDate,
                Doctor = doctor,
                Patient = patient
            };

            await _prescriptionRepository.AddPrescription(prescription, cancellationToken);
            foreach (var prescriptionDetail in dto.PrescriptionDetails)
            {
                var medicament =
                    await _medicamentService.GetMedicament(prescriptionDetail.MedicamentName, cancellationToken);
                var prescriptionMedicament = new PrescriptionMedicament
                {
                    Medicament = medicament,
                    Prescription = prescription,
                    Dose = prescriptionDetail.Dose,
                    Details = prescriptionDetail.Details
                };
                await _prescriptionRepository.AddPrescriptionMedicament(prescriptionMedicament, cancellationToken);
            }

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _unitOfWork.DisposeAsync(); 
        }
    }
}