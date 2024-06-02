using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Prescription.DTOs;

public class PrescribeDto
{
    [JsonPropertyName("patient"), Required]
    public PatientData PatientData { get; init; } = null!;

    [JsonPropertyName("medicaments"), Required]
    public List<PrescriptionDetail> PrescriptionDetails { get; init; } = null!;

    public DateTime Date { get; init; }

    public DateTime DueDate { get; init; }
}


public class PatientData
{
    [Required]
    public string FirstName { get; init; } = null!;

    [Required]
    public string LastName { get; init; } = null!;

    [Required]
    public DateTime BirthDate { get; init; }
}

public class PrescriptionDetail
{
    [Required]
    public string MedicamentName { get; init; } = null!;

    public int? Dose { get; init; }

    [Required]
    public string Details { get; init; } = null!;
}