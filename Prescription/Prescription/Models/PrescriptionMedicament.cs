namespace Prescription.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; init; }
    public int IdPrescription { get; init; }
    public Medicament Medicament { get; init; } = null!;
    public Prescription Prescription { get; init; } = null!;
    public int? Dose { get; init; }
    public string Details { get; init; } = null!;
}