using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Trip.Context;

namespace Trip.Repositories;

public interface ITripRepository
{
    Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken);
}

public class TripRepository : ITripRepository
{
    private readonly IConfiguration _configuration;
    private readonly TripAppContext _appContext;
    
    public TripRepository(IConfiguration configuration, TripAppContext appContext)
    {
        _configuration = configuration;
        _appContext = appContext;
        Initialize();
    }

    private void Initialize()
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using var createCommand = new SqlCommand();

        var createFile = new FileInfo("cw5_create.sql");

        createCommand.CommandText = $"IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE [name] = 'trip') " +
                                    $"BEGIN {createFile.OpenText().ReadToEnd()} END;";
        createCommand.Connection = connection;
        createCommand.ExecuteNonQuery();
    }

    public async Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken)
    {
        var trips = await _appContext
            .Trips
            .OrderByDescending(trip => trip.DateFrom)
            .ToListAsync(cancellationToken);

        return trips;
    }
}