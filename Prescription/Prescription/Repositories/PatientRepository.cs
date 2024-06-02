using Microsoft.EntityFrameworkCore;
using Prescription.Context;
using Prescription.DTOs;
using Prescription.Models;

namespace Prescription.Repositories;

public interface IPatientRepository
{
    Task<Patient> GetOrAddPatient(PatientData patientData, CancellationToken cancellationToken);
}

public class PatientRepository(PrescriptionAppContext context) : IPatientRepository
{
    public async Task<Patient> GetOrAddPatient(PatientData patientData, CancellationToken cancellationToken)
    {
        var existingPatient = await context.Patients.FirstOrDefaultAsync(
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
        await context.Patients.AddAsync(newPatient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newPatient;
    }
}