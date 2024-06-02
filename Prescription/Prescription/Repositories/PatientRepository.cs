using Microsoft.EntityFrameworkCore;
using Prescription.Context;
using Prescription.DTOs;
using Prescription.Models;

namespace Prescription.Repositories;

public interface IPatientRepository
{
    Task<Patient> GetOrAddPatient(PatientData patientData, CancellationToken cancellationToken);
    Task<Patient> GetPatient(int idPatient, CancellationToken cancellationToken);
}

public class PatientRepository(PrescriptionAppContext context) : IPatientRepository
{
    private readonly PrescriptionAppContext _context = context;

    public async Task<Patient> GetOrAddPatient(PatientData patientData, CancellationToken cancellationToken)
    {
        var existingPatient = await _context.Patients.FirstOrDefaultAsync(
            patient =>
                patient.BirthDate == patientData.BirthDate &&
                patient.LastName == patientData.LastName &&
                patient.FirstName == patientData.FirstName,
            cancellationToken);

        if (existingPatient != null)
        {
            return existingPatient;
        }

        var newPatient = new Patient(patientData);
        await _context.Patients.AddAsync(newPatient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newPatient;
    }

    public async Task<Patient> GetPatient(int idPatient, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .Include(patient => patient.Prescriptions)
            .ThenInclude(prescription => prescription.Medicaments)
            .ThenInclude(prescriptionMedicament => prescriptionMedicament.Medicament)
            .Include(patient => patient.Prescriptions)
            .ThenInclude(prescription => prescription.Doctor)
            .SingleAsync(patient => patient.IdPatient == idPatient, cancellationToken);
    }
}