namespace Warehouse;

public interface IWarehouseService
{
    Task<int> AddProduct(AddProductModel model);
    Task<bool> Validate(AddProductModel model);
    Task<int> AddProductWithProcedure(AddProductModel model);
}

public class WarehouseService(IWarehouseRepository warehouseRepository) : IWarehouseService
{
    public async Task<int> AddProduct(AddProductModel model)
    {
        var orderId = await warehouseRepository.GetOrderId(model);
        if (orderId == null)
        {
            throw new InvalidOperationException("Order not found, or already completed");
        }

        return await warehouseRepository.AddProduct(model, orderId.Value);
    }

    public async Task<bool> Validate(AddProductModel model)
    {
        return await warehouseRepository.Validate(model);
    }

    public async Task<int> AddProductWithProcedure(AddProductModel model)
    {
        return await warehouseRepository.AddProductWithProcedure(model);
    }
}