using Microsoft.EntityFrameworkCore;
using Trip.Context;
using Trip.Models;

namespace Trip.Repositories;

public interface ITripRepository
{
    Task AssignClient(Client client, Models.Trip trip, DateTime? paymentDate, CancellationToken cancellationToken);
    Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken);
    Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken);
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

    public async Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken)
    {
        var trips = await appContext
            .Trips
            .Include(t => t.ClientTrips)
            .OrderByDescending(trip => trip.DateFrom)
            .ToListAsync(cancellationToken);

        return trips;
    }
}