using Prescription.DTOs;

namespace Prescription.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public DateTime BirthDate { get; init; }

    public ICollection<Prescription> Prescriptions = new List<Prescription>();

    public Patient()
    {
        
    }

    public Patient(PatientData patientData)
    {
        FirstName = patientData.FirstName;
        LastName = patientData.LastName;
        BirthDate = patientData.BirthDate;
    }
}