using Microsoft.AspNetCore.Mvc;

namespace Warehouse;

[Route("/warehouse")]
[ApiController]
public class WarehouseController(IWarehouseService warehouseService) : ControllerBase
{
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct(AddProductModel model)
    {
        await warehouseService.AddProduct(model);
        return StatusCode(StatusCodes.Status201Created);
    }
}