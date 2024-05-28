using Trip.Models;
using Trip.Repositories;

namespace Trip.Service;

public interface ITripService
{
    Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken);
    Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken);
    Task AssignClient(AssignClientDto dto, CancellationToken cancellationToken);
}

public class TripService(ITripRepository tripRepository, IClientService clientService) : ITripService
{
    public async Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken)
    {
        return await tripRepository.GetTrips(cancellationToken);
    }

    public async Task AssignClient(AssignClientDto dto, CancellationToken cancellationToken)
    {
        var client = new Client(dto);
        client = await clientService.GetOrAddClient(client, cancellationToken);
        ArgumentNullException.ThrowIfNull(client.IdClient);

        var isAlreadyAssigned = client.ClientTrips.Any(trip => trip.IdTrip == dto.IdTrip);
        if (isAlreadyAssigned)
        {
            throw new Exception($"Client id {client.IdClient} is already assigned to the trip id {dto.IdTrip}");
        }

        var trip = await GetTrip(dto.IdTrip, cancellationToken);
        if (trip.Name != dto.TripName)
        {
            throw new Exception($"Trip name {dto.TripName} does not match the value retrieved from database {trip.Name}");
        }

        if (trip.MaxPeople == trip.ClientTrips.Count)
        {
            throw new Exception($"Trip id {dto.IdTrip} is fully booked");
        }

        await tripRepository.AssignClient(client, trip, dto.PaymentDate, cancellationToken);
    }

    public async Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken)
    {
        return await tripRepository.GetTrip(idTrip, cancellationToken);
    }
}