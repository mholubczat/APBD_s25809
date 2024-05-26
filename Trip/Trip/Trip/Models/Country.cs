using System.ComponentModel.DataAnnotations;

namespace Trip.Models;

public sealed partial class Country
{
    [Key, Required] 
    public required int IdCountry { get; init; }

    [Required, MaxLength(120)]
    public required string Name { get; init; } = null!;

    public ICollection<Trip> IdTrips { get; init; } = new List<Trip>();
}
