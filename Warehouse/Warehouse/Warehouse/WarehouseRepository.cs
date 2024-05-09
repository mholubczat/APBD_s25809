using System.Data.SqlClient;

namespace Warehouse;

public interface IWarehouseRepository
{
    Task<int> AddProduct(AddProductModel model, int orderId);
    Task<bool> Validate(AddProductModel model);
    Task<int?> GetOrderId(AddProductModel model);
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

        var fileInfo = new FileInfo("create.sql");
        command.CommandText = "IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE Name = 'Warehouse') BEGIN " +
                              fileInfo.OpenText().ReadToEnd() + " END;";
        command.Connection = connection;

        command.ExecuteNonQuery();
    }

    public async Task<int> AddProduct(AddProductModel model, int orderId)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.CommandText = "BEGIN TRANSACTION " +
                              "UPDATE [Order] SET FulfilledAt = GETDATE()" +
                              "WHERE IdOrder = @OrderId; " +
                              "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                              "SELECT @WarehouseId, @ProductId, @OrderId, @Amount, @Amount * p.Price, GETDATE() " +
                              "FROM Product p WHERE p.IdProduct = @ProductId; " +
                              "COMMIT; " +
                              "SELECT SCOPE_IDENTITY();";
        command.Parameters.AddWithValue("@WarehouseId", model.WarehouseId);
        command.Parameters.AddWithValue("@ProductId", model.ProductId);
        command.Parameters.AddWithValue("@OrderId", orderId);
        command.Parameters.AddWithValue("@Amount", model.Amount);
        command.Connection = connection;

        var productWarehouseId = await command.ExecuteScalarAsync();
        if (productWarehouseId == null || productWarehouseId == DBNull.Value)
        {
            throw new InvalidOperationException("Failed to retrieve productWarehouseId");
        }

        return Convert.ToInt32(productWarehouseId);
    }

    public async Task<bool> Validate(AddProductModel model)
    {
        await using var connection =
            new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var validationCommand = new SqlCommand();
        validationCommand.CommandText =
            "SELECT 1 FROM Product CROSS JOIN Warehouse WHERE IdProduct = @ProductId AND IdWarehouse = @WarehouseId";
        validationCommand.Parameters.AddWithValue("@ProductId", model.ProductId);
        validationCommand.Parameters.AddWithValue("@WarehouseId", model.WarehouseId);
        validationCommand.Connection = connection;

        var result = (int?) await validationCommand.ExecuteScalarAsync();

        return result == 1;
    }

    public async Task<int?> GetOrderId(AddProductModel model)
    {
        await using var connection =
            new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.CommandText =
            "SELECT o.IdOrder FROM [Order] o LEFT JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder " +
            "WHERE o.IdProduct = @ProductId AND o.Amount = @Amount AND o.CreatedAt < @CreatedAt AND pw.IdOrder IS NULL";
        command.Parameters.AddWithValue("@ProductId", model.ProductId);
        command.Parameters.AddWithValue("@Amount", model.Amount);
        command.Parameters.AddWithValue("@CreatedAt", model.CreatedAt);
        command.Connection = connection;

        var orderId = (int?) await command.ExecuteScalarAsync();        
        return orderId;
    }
}