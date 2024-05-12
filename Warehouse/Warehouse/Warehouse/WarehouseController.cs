using System.Data.SqlClient;
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

        int insertedId;
        try
        {
            insertedId = await warehouseService.AddProduct(model);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message); 
        }

        return Ok(insertedId);
    }

    [HttpPost("add-product-with-procedure")]
    public async Task<IActionResult> AddProductWithProcedure(AddProductModel model)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        int insertedId;

        try
        {
            insertedId = await warehouseService.AddProductWithProcedure(model);
        }
        catch (SqlException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message); 
        }

        return Ok(insertedId);
    }
}