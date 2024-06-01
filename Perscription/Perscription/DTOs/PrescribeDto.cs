using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prescription.DTOs;

public class PerscribeDto
{
    [JsonPropertyName("patient"), Required]
    public PatientData PatientData { get; set; } = null!;

    [JsonPropertyName("medicaments"), Required]
    public List<PerscriptionDetail> PerscriptionDetails { get; set; } = null!;

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

public class PerscriptionDetail
{
    [Required]
    public string MedicamentName { get; set; } = null!;

    public string? Dose { get; set; }

    [Required]
    public string Details { get; set; } = null!;
}