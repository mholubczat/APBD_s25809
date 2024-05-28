using System.ComponentModel.DataAnnotations;

namespace Trip.Models;

public sealed partial class Client
{
    [Key, Required] 
    public int? IdClient { get; init; }

    [Required, MaxLength(120)]
    public string FirstName { get; init; } = null!;

    [Required, MaxLength(120)]
    public string LastName { get; init; } = null!;

    [Required, MaxLength(120)]
    public string Email { get; init; } = null!;

    [Required, MaxLength(120)]
    public string Telephone { get; init; } = null!;

    [Required, MaxLength(120)]
    public string Pesel { get; init; } = null!;

    public ICollection<ClientTrip> ClientTrips { get; init; } = new List<ClientTrip>();

    public Client()
    {
    }
    
    public Client(AssignClientDto dto)
    {
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Email = dto.Email;
        Telephone = dto.Telephone;
        Pesel = dto.Pesel;
    }
}