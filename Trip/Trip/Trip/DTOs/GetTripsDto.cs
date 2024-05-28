using Trip.Models;

namespace Trip.DTOs;

public class GetTripsDto
{
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required DateTime DateFrom { get; init; }

    public required DateTime DateTo { get; init; }
    
    public required int MaxPeople { get; init; }

    public ICollection<ClientData> Clients { get; init; } = new List<ClientData>();

    public ICollection<CountryData> Countries { get; init; } = new List<CountryData>();
}

public class ClientData
{
    public string FirstName { get; init; } = null!;
    
    public string LastName { get; init; } = null!;
}

public class CountryData
{
    public string Name { get; init; } = null!;
}