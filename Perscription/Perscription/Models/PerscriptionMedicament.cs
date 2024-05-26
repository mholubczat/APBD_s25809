namespace Perscription.Models;

public class PerscriptionMedicament
{
    public Medicament Medicament { get; set; }
    public Perscription Perscription { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}