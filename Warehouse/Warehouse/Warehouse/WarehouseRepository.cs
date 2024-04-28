using System.Data.SqlClient;

namespace Warehouse;

public interface IWarehouseRepository
{
    Task<int> AddProduct(AddProductModel model);
}

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        Initialize();
    }

    private void Initialize()
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using var command = new SqlCommand();
        command.Connection = connection;

        command.ExecuteNonQuery();
    }

    public async Task<int> AddProduct(AddProductModel model)
    {
        throw new NotImplementedException();
    }
}