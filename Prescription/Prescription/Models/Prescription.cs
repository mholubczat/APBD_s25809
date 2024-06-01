namespace Prescription.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public Patient Patient { get; set; } = null!;

    public ICollection<PrescriptionMedicament> Medicaments = new List<PrescriptionMedicament>();
}
