namespace Prescription.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; init; }
    public DateTime DueDate { get; init; }
    public int IdDoctor { get; init; }
    public int IdPatient { get; init; }
    public Doctor Doctor { get; init; } = null!;
    public Patient Patient { get; init; } = null!;

    public ICollection<PrescriptionMedicament> Medicaments = new List<PrescriptionMedicament>();
}
