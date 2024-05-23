using Microsoft.AspNetCore.Mvc;
using Trip.Models;
using Trip.Service;

namespace Trip.Controllers;

[Route("/api/trips"), ApiController]
public class TripController(ITripService tripService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTrips(CancellationToken cancellationToken)
    {
        var trips = await tripService.GetTrips(cancellationToken);

        return Ok(trips);
    }

    [HttpPost( "{idTrip:int}/clients")]
    public async Task<IActionResult> AssignClient(int idTrip, AssignClientDto dto, CancellationToken cancellationToken)
    {
        if (idTrip != dto.IdTrip)
        {
            return BadRequest($"Trip id {idTrip} does not match id from body request {dto.IdTrip}");
        }

        try
        {
            await tripService.AssignClient(dto, cancellationToken);
        }
        catch (InvalidOperationException)
        {
            return BadRequest($"Trip id {idTrip} does not exist");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        return Created();
    }
}