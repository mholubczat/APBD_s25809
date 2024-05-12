using System.Data.SqlClient;

namespace Warehouse;

public interface IWarehouseRepository
{
    Task<int> AddProduct(AddProductModel model, int orderId);
    Task<bool> Validate(AddProductModel model);
    Task<int?> GetOrderId(AddProductModel model);
    Task<int> AddProductWithProcedure(AddProductModel model);
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
        using var createCommand = new SqlCommand();

        var createFile = new FileInfo("create.sql");

        createCommand.CommandText = $"IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE [name] = 'Warehouse') " +
                                    $"BEGIN {createFile.OpenText().ReadToEnd()} END; " +
                                    $"SELECT 1 FROM sys.procedures WHERE [name] = 'AddProductToWarehouse';";
        createCommand.Connection = connection;

        var procedureExists = (int?)createCommand.ExecuteScalar() == 1;

        if (procedureExists)
        {
            return;
        }

        using var procedureCommand = new SqlCommand();
        var procedureFile = new FileInfo("proc.sql");

        procedureCommand.CommandText = procedureFile.OpenText().ReadToEnd();
        procedureCommand.Connection = connection;
        procedureCommand.ExecuteNonQuery();
    }

    public async Task<int> AddProduct(AddProductModel model, int orderId)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.CommandText = """
                              BEGIN TRANSACTION
                              UPDATE [Order] SET FulfilledAt = GETDATE()
                              WHERE IdOrder = @OrderId;

                              INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                              SELECT @WarehouseId, @ProductId, @OrderId, @Amount, @Amount * p.Price, GETDATE()
                              FROM Product p WHERE p.IdProduct = @ProductId;
                              COMMIT;

                              SELECT SCOPE_IDENTITY();
                              """;
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
            "SELECT 1 FROM Product p CROSS JOIN Warehouse w WHERE p.IdProduct = @ProductId AND w.IdWarehouse = @WarehouseId";
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
        command.CommandText ="""
            SELECT o.IdOrder FROM [Order] o LEFT JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder
            WHERE o.IdProduct = @ProductId AND o.Amount = @Amount AND o.CreatedAt < @CreatedAt AND pw.IdOrder IS NULL
            """;
        command.Parameters.AddWithValue("@ProductId", model.ProductId);
        command.Parameters.AddWithValue("@Amount", model.Amount);
        command.Parameters.AddWithValue("@CreatedAt", model.CreatedAt);
        command.Connection = connection;

        var orderId = (int?) await command.ExecuteScalarAsync();        
        return orderId;
    }

    public async Task<int> AddProductWithProcedure(AddProductModel model)
    {
        await using var connection =
            new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        await using var command = new SqlCommand();

        command.CommandText = "EXEC AddProductToWarehouse @IdProduct, @IdWarehouse, @Amount, @CreatedAt";
        command.Parameters.AddWithValue("@IdProduct", model.ProductId);
        command.Parameters.AddWithValue("@IdWarehouse", model.WarehouseId);
        command.Parameters.AddWithValue("@Amount", model.Amount);
        command.Parameters.AddWithValue("@CreatedAt", model.CreatedAt);
        command.Connection = connection;

        var productWarehouseId = await command.ExecuteScalarAsync();
        if (productWarehouseId == null || productWarehouseId == DBNull.Value)
        {
            throw new InvalidOperationException("Failed to retrieve productWarehouseId");
        }

        return Convert.ToInt32(productWarehouseId);
    }
}