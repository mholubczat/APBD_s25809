using Microsoft.AspNetCore.Mvc;

namespace Warehouse;

[Route("/warehouse")]
[ApiController]
public class WarehouseController(IWarehouseService warehouseService) : ControllerBase
{
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct(AddProductModel model)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        
        if (await warehouseService.Validate(model) == false)
        {
            return BadRequest("Invalid request - product or warehouse does not exist in database");
        }
        
        var insertedId = await warehouseService.AddProduct(model);
        if (insertedId == null)
        {
            return BadRequest("Order not found, or already completed");
        }
        
        return Ok(insertedId.Value);
    }
}