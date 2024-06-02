using Prescription.DTOs;
using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services;

public interface IPatientService
{
    Task<Patient> GetOrAddPatient(PatientData patientData, CancellationToken cancellationToken);
}

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<Patient> GetOrAddPatient(PatientData patientData, CancellationToken cancellationToken)
    {
        return await patientRepository.GetOrAddPatient(patientData, cancellationToken);
    }
}