using Microsoft.EntityFrameworkCore;
using Trip.Context;
using Trip.DTOs;
using Trip.Models;

namespace Trip.Repositories;

public interface ITripRepository
{
    Task AssignClient(Client client, Models.Trip trip, DateTime? paymentDate, CancellationToken cancellationToken);
    Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken);
    Task<IList<GetTripsDto>> GetTrips(CancellationToken cancellationToken);
}

public class TripRepository(TripAppContext appContext) : ITripRepository
{
    public async Task AssignClient(Client client, Models.Trip trip, DateTime? paymentDate, CancellationToken cancellationToken)
    {
        var clientTrip = new ClientTrip
        {
            RegisteredAt = DateTime.Now,
            PaymentDate = paymentDate,
            Client = client,
            Trip = trip
        };

        appContext
            .ClientTrips
            .Add(clientTrip);
        await appContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken)
    {
        var trip = await appContext
            .Trips
            .Include(t => t.ClientTrips)
            .SingleAsync(trip => trip.IdTrip == idTrip, cancellationToken);

        return trip;
    }

    public async Task<IList<GetTripsDto>> GetTrips(CancellationToken cancellationToken)
    {
        var trips = await appContext
            .Trips
            .Include(trip => trip.ClientTrips)
            .ThenInclude(clientTrip => clientTrip.Client)
            .Include(trip => trip.Countries)
            .Select(trip => new GetTripsDto
            {
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,
                Clients = trip.ClientTrips
                    .Select(clientTrip =>
                        new ClientData
                        {
                            FirstName = clientTrip.Client.FirstName,
                            LastName = clientTrip.Client.LastName
                        })
                    .ToList(),
                Countries = trip.Countries.Select(country => new CountryData { Name = country.Name }).ToList()
            })
            .OrderByDescending(trip => trip.DateFrom)
            .ToListAsync(cancellationToken);

        return trips;
    }
}