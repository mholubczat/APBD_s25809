namespace Perscription.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public ICollection<Perscription> Perscriptions = new List<Perscription>();
}