using System.ComponentModel.DataAnnotations;

namespace Trip.Models;

public class AssignClientDto
{
    [Required, MaxLength(120)]
    public required string FirstName { get; init; }

    [Required, MaxLength(120)]
    public required string LastName { get; init; }

    [Required, MaxLength(120)]
    public required string Email { get; init; }

    [Required, MaxLength(120)]
    public required string Telephone { get; init; }

    [Required, MaxLength(120)]
    public required string Pesel { get; init; }

    [Required]
    public int IdTrip { get; init; }

    [Required]
    public required string TripName { get; init; }

    public DateTime? PaymentDate { get; init; }
}