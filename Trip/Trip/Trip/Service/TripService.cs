using Trip.Repositories;

namespace Trip.Service;

public interface ITripService
{
    Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken);
}

public class TripService(ITripRepository tripRepository) : ITripService
{
    public async Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken)
    {
        return await tripRepository.GetTrips(cancellationToken);
    }
}