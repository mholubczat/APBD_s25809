using System.ComponentModel.DataAnnotations;

namespace Trip.Models;

public sealed partial class ClientTrip
{
    public int IdClient { get; init; }

    public int IdTrip { get; init; }

    [Required] 
    public required DateTime RegisteredAt { get; init; }

    public DateTime? PaymentDate { get; init; }

    public Client Client { get; init; } = null!;

    public Trip Trip { get; init; } = null!;
}
