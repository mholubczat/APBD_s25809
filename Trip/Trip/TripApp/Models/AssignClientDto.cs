using System.ComponentModel.DataAnnotations;

namespace Trip.Models;

public class AssignClientDto
{
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }

    public required string Telephone { get; init; }

    public required string Pesel { get; init; }

    [Required]
    public Client Client => new()
    {
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        Telephone = Telephone,
        Pesel = Pesel
    };

    [Required]
    public int IdTrip { get; init; }

    [Required]
    public required string TripName { get; init; }

    private Trip _trip  = null!;
    public Trip Trip
    {
        get => _trip;
        set
        {
            if (value.Name != TripName)
            {
                throw new Exception($"Trip name {value.Name} does not match value from database {TripName}");
            }

            _trip = value;
        }
    }

    public DateTime? PaymentDate { get; init; }
}