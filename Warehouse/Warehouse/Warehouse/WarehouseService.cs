namespace Warehouse;

public interface IWarehouseService
{
    public Task<int> AddProduct(AddProductModel model);
}

public class WarehouseService(IWarehouseRepository warehouseRepository) : IWarehouseService
{
    public async Task<int> AddProduct(AddProductModel model)
    {
        return await warehouseRepository.AddProduct(model);
    }
}