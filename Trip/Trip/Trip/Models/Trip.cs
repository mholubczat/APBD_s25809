using System.ComponentModel.DataAnnotations;

namespace Trip.Models;

public sealed partial class Trip
{
    [Key, Required]
    public int IdTrip { get; init; }

    [Required, MaxLength(120)]
    public string Name { get; init; } = null!;

    [Required, MaxLength(220)]
    public string Description { get; init; } = null!;

    [Required]
    public DateTime DateFrom { get; init; }

    [Required]
    public DateTime DateTo { get; init; }

    [Required]
    public int MaxPeople { get; init; }

    public ICollection<ClientTrip> ClientTrips { get; init; } = new List<ClientTrip>();

    public ICollection<Country> IdCountries { get; init; } = new List<Country>();
}
