using System.Data.SqlClient;
using Microsoft.Extensions.Options;

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

        var fileInfo = new FileInfo("create.sql");
        command.CommandText = "IF NOT EXISTS SELECT 1 FROM sys.tables WHERE Name = 'Warehouse' BEGIN " +
                              fileInfo.OpenText().ReadToEnd() + " END";
        command.Connection = connection;

        command.ExecuteNonQuery();
    }

    public async Task<int> AddProduct(AddProductModel model)
    {
        if (Validate(model) == false)
        {
            return 0;
        }

        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.CommandText = "IF EXISTS SELECT 1 FROM Product WHERE IdProduct = @ProductId " +
                              "AND EXISTS SELECT 1 FROM Warehouse WHERE IdWarehouse = @WarehouseId " +
                              "AND EXISTS SELECT 1 FROM Order o " +
                                  "LEFT JOIN Product_Warehouse pw ON o.OrderId = pw.OrderId " +
                                  "WHERE o.IdProduct = @ProductId AND o.Amount = @Amount AND o.CreatedAt < @CreatedAt AND pw.OrderId IS NULL " +
                              "BEGIN " +
                                  "DECLARE @OrderId INT = SELECT OrderId FROM ORDER WHERE o.IdProduct = @ProductId AND o.Amount = @Amount AND o.CreatedAt < @CreatedAt" +  
                                  "DECLARE @OrderId INT = SELECT OrderId FROM ORDER WHERE o.IdProduct = @ProductId AND o.Amount = @Amount AND o.CreatedAt < @CreatedAt" + 
                                  "BEGIN TRANSACTION " +
                                      "UPDATE ORDER SET FullfilledAt = GETDATE() " +
                                      "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                                          "SELECT @WarehouseId, @ProductId, @OrderId, @Amount, @Amount * p.Price, GETDATE()" +
                                              "FROM Product p WHERE p.IdProduct = @ProductId";

        return 1;
    }

    private static bool Validate(AddProductModel model)
    {
        return model is { ProductId: >= 1, WarehouseId: >= 1, Amount: >= 1 };
    }
}