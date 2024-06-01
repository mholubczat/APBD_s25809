namespace Prescription.Models;

public class Prescription
{
    public int IdPerscription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public Patient Patient { get; set; } = null!;

    public ICollection<PerscriptionMedicament> Medicaments = new List<PerscriptionMedicament>();
}
