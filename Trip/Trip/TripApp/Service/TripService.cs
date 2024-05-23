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
        await clientService.AddClient(dto.Client, cancellationToken);
        ArgumentNullException.ThrowIfNull(dto.Client.IdClient);

        var isAlreadyAssigned = dto.Client.ClientTrips.Any(trip => trip.IdTrip == dto.IdTrip);
        if (isAlreadyAssigned)
        {
            throw new Exception($"Client id {dto.Client.IdClient} is already assigned to the trip id {dto.IdTrip}");
        }

        dto.Trip = await GetTrip(dto.IdTrip, cancellationToken);

        await tripRepository.AssignClient(dto, cancellationToken);
    }

    public async Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken)
    {
        return await tripRepository.GetTrip(idTrip, cancellationToken);
    }
}