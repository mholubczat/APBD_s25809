using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Trip.Models;

public sealed partial class Country
{
    [Key, Required, JsonIgnore] 
    public required int IdCountry { get; init; }

    [Required, MaxLength(120)]
    public required string Name { get; init; } = null!;

    [JsonIgnore]
    public ICollection<Trip> IdTrips { get; init; } = new List<Trip>();
}
