namespace Warehouse;

public interface IWarehouseService
{
    Task<int?> AddProduct(AddProductModel model);
    Task<bool> Validate(AddProductModel model);
}

public class WarehouseService(IWarehouseRepository warehouseRepository) : IWarehouseService
{
    public async Task<int?> AddProduct(AddProductModel model)
    {
        var orderId = await warehouseRepository.GetOrderId(model);
        if (orderId == null)
        {
            return null;
        }

        return await warehouseRepository.AddProduct(model, orderId.Value);
    }

    public async Task<bool> Validate(AddProductModel model)
    {
        return await warehouseRepository.Validate(model);
    }
}