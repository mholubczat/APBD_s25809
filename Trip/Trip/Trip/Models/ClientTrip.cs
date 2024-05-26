using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trip.Models;

public sealed partial class ClientTrip
{
    public int IdClient { get; init; }

    public int IdTrip { get; init; }

    [Required] 
    public required DateTime RegisteredAt { get; init; }

    public DateTime? PaymentDate { get; init; }

    public Client Client { get; init; } = null!;

    private readonly Trip _trip = null!;

    public Trip Trip
    {
        get => _trip;
        init
        {
            if (value.ClientTrips.Count > value.MaxPeople)
            {
                throw new ValidationException($"Trip is full: {value.MaxPeople} assigned");
            }

            _trip = value;
        }
    }
}
