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
    }

    public async Task<int> AddProduct(AddProductModel model)
    {
        throw new NotImplementedException();
    }
}