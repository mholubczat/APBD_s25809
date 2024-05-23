using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Models;

public sealed partial class ClientTrip
{
    [ForeignKey(nameof(Client)), Required] 
    public int IdClient => IdClientNavigation.IdClient;

    [ForeignKey(nameof(Trip)), Required] 
    public int IdTrip => IdTripNavigation.IdTrip;

    [Required]
    public required DateTime RegisteredAt { get; init; }

    public DateTime? PaymentDate { get; init; }

    public Client IdClientNavigation { get; init; } = null!;
    
    public Trip IdTripNavigation { get; init; } = null!;
}
