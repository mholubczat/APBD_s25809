namespace Prescription.Models;

public class Doctor
{
    public int IdDoctor { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    
    public ICollection<Prescription> Prescriptions = new List<Prescription>();
}