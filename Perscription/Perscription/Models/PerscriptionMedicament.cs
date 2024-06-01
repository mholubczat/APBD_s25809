namespace Perscription.Models;

public class PerscriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPerscription { get; set; }
    public Medicament Medicament { get; set; }
    public Perscription Perscription { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}