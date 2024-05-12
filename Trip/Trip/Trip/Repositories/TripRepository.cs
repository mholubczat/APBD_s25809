using Microsoft.Data.SqlClient;

namespace Trip.Repositories;

public interface ITripRepository
{
    
}

public class TripRepository : ITripRepository
{
    private readonly IConfiguration _configuration;
    
    public TripRepository(IConfiguration configuration)
    {
        _configuration = configuration;
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
}