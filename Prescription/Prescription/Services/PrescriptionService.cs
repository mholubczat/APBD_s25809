using Prescription.DTOs;
using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services;

public interface IPrescriptionService
{
    Task AddPrescription(int idDoctor, PrescribeDto dto, CancellationToken cancellationToken);
}

public class PrescriptionService(
    IPrescriptionRepository prescriptionRepository,
    IDoctorService doctorService, 
    IMedicamentService medicamentService, 
    IPatientService patientService) : IPrescriptionService
{
    public async Task AddPrescription(int idDoctor, PrescribeDto dto, CancellationToken cancellationToken)
    {
        var getDoctorTask = doctorService.GetDoctor(idDoctor, cancellationToken);
        var getMedicamentTasks = dto.PrescriptionDetails
            .Select(detail => medicamentService.GetMedicament(detail.MedicamentName, cancellationToken))
            .ToList();
        var getPatientTask = patientService.GetOrAddPatient(dto.PatientData, cancellationToken);

        var readFromDatabase = getMedicamentTasks.ToList<Task>();
        readFromDatabase.Add(getDoctorTask);
        readFromDatabase.Add(getPatientTask);

        await Task.WhenAll(readFromDatabase);
        var prescription = new Models.Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            Doctor = getDoctorTask.Result,
            Patient = getPatientTask.Result
        };

        await prescriptionRepository.AddPrescription(prescription, cancellationToken);
        foreach (var medicament in getMedicamentTasks.Select(task => task.Result))
        {

            var detail = dto.PrescriptionDetails.First(detail => detail.MedicamentName == medicament.Name);
            var prescriptionMedicament = new PrescriptionMedicament
            {
                Medicament = medicament,
                Prescription = prescription,
                Dose = detail.Dose,
                Details = detail.Details
            };
            await prescriptionRepository.AddPrescriptionMedicament(prescriptionMedicament, cancellationToken);
        }
    }
}