namespace Prescription.Models;

public class Medicament
{
    public int IdMedicament { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Type { get; init; } = null!;

    public ICollection<PrescriptionMedicament> Prescriptions = new List<PrescriptionMedicament>();
}