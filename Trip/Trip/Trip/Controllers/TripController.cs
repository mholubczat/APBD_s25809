using Microsoft.AspNetCore.Mvc;
using Trip.Service;

namespace Trip.Controllers;

[Route("/api"), ApiController]
public class TripController(ITripService tripService) : ControllerBase
{
    [HttpGet("trips")]
    public async Task<IActionResult> GetTrips(CancellationToken cancellationToken)
    {
        var trips = await tripService.GetTrips(cancellationToken);
        return Ok(trips);
    }
    
    [HttpDelete("clients/{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient, CancellationToken cancellationToken)
    {
        var trips = await tripService.GetTrips(cancellationToken);
        return NoContent();
    }
}