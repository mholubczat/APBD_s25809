using System.ComponentModel.DataAnnotations;

namespace Trip.Models;

public sealed partial class Trip
{
    [Key, Required]
    public required int IdTrip { get; init; }

    [Required, MaxLength(120)]
    public required string Name { get; init; }

    [Required, MaxLength(220)]
    public required string Description { get; init; }

    [Required]
    public required DateTime DateFrom { get; init; }

    [Required]
    public required DateTime DateTo { get; init; }

    [Required]
    public required int MaxPeople { get; init; }

    public ICollection<ClientTrip> ClientTrips { get; init; } = new List<ClientTrip>();

    public ICollection<Country> IdCountries { get; init; } = new List<Country>();
}
