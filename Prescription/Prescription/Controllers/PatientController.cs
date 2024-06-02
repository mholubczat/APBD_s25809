using Microsoft.AspNetCore.Mvc;
using Prescription.DTOs;
using Prescription.Services;

namespace Prescription.Controllers;

[Route("/api/patient"), ApiController]
public class PatientController(IPatientService patientService) : ControllerBase
{
    private readonly IPatientService _patientService = patientService;

    [HttpGet("{idPatient:int}")]
    public async Task<IActionResult> GetPatient(int idPatient, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await _patientService.GetPatient(idPatient, cancellationToken);
            var result = new PatientDataDto
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Prescriptions = patient.Prescriptions.Select(
                    prescription => new PrescriptionData
                    {
                        IdPrescription = prescription.IdPrescription,
                        Date = prescription.Date,
                        DueDate = prescription.DueDate,
                        Medicaments = prescription.Medicaments.Select(
                            prescriptionMedicament => new MedicamentData
                            {
                                IdMedicament = prescriptionMedicament.IdMedicament,
                                Name = prescriptionMedicament.Medicament.Name,
                                Description = prescriptionMedicament.Medicament.Description,
                                Dose = prescriptionMedicament.Dose,
                                Details = prescriptionMedicament.Details
                            }
                        ).ToList(),
                        Doctor = new DoctorData
                        {
                            IdDoctor = prescription.Doctor.IdDoctor,
                            FirstName = prescription.Doctor.FirstName,
                            LastName = prescription.Doctor.LastName,
                            Email = prescription.Doctor.Email
                        }
                    })
                    .OrderBy(data => data.DueDate)
                    .ToList()
            };

            return Ok(result);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}