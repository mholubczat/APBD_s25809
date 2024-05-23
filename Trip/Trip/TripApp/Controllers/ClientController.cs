using Microsoft.AspNetCore.Mvc;
using Trip.Service;

namespace Trip.Controllers;

[Route("/api/clients"), ApiController]
public class ClientController(IClientService clientService) : ControllerBase
{
    [HttpDelete("{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient, CancellationToken cancellationToken)
    {
        try
        {
            await clientService.DeleteClient(idClient, cancellationToken);
        }
        catch (Exception exception)
        {
            BadRequest(exception.Message);
        }

        return NoContent();
    }
}