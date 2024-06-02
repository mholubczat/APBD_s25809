using Prescription.DTOs;

namespace Prescription.Models;

public class Patient(PatientData patientData)
{
    public int IdPatient { get; init; }
    public string FirstName { get; init; } = patientData.FirstName;
    public string LastName { get; init; } = patientData.LastName;
    public DateTime BirthDate { get; init; } = patientData.BirthDate;

    public ICollection<Prescription> Prescriptions = new List<Prescription>();
}