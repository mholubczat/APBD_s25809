namespace Perscription.Models;

public class Perscription
{
    public int IdPerscription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public Patient Patient { get; set; } = null!;

    public ICollection<PerscriptionMedicament> Medicaments = new List<PerscriptionMedicament>();
}
