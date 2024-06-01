using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prescription.DTOs;

public class PrescribeDto
{
    [JsonPropertyName("patient"), Required]
    public PatientData PatientData { get; set; } = null!;

    [JsonPropertyName("medicaments"), Required]
    public List<PrescriptionDetail> PrescriptionDetails { get; set; } = null!;

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }
}


public class PatientData
{
    [Required]
    public string PatientFirstName { get; set; } = null!;

    [Required]
    public string PatientLastName { get; set; } = null!;

    [Required]
    public DateTime PatientBirthDate { get; set; }
}

public class PrescriptionDetail
{
    [Required]
    public string MedicamentName { get; set; } = null!;

    public string? Dose { get; set; }

    [Required]
    public string Details { get; set; } = null!;
}