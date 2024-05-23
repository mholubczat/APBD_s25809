using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Models;

public sealed partial class ClientTrip
{
    [ForeignKey(nameof(Client)), Required] 
    public int IdClient { get; init; }

    [ForeignKey(nameof(Trip)), Required]
    public int IdTrip { get; init; }

    [Required]
    public DateTime RegisteredAt { get; init; }

    public DateTime? PaymentDate { get; init; }

    public Client IdClientNavigation { get; init; } = null!;
    
    public Trip IdTripNavigation { get; init; } = null!;
}
