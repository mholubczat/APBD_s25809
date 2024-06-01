using Microsoft.AspNetCore.Mvc;
using Prescription.DTOs;
using Prescription.Services;

namespace Prescription.Controller;

[ApiController]
public class PerscriptionController(IMedicamentService medicamentService) : ControllerBase
{
    [Route("{idDoctor:int}/perscribe")]
    public async Task<IActionResult> Perscribe(int idDoctor, PerscribeDto dto, CancellationToken cancellationToken)
    {
        var validationResult = Validate(dto);
        if (validationResult.isValid == false)
        {
            return BadRequest(validationResult.errorMsg);
        }

        try
        {
            await medicamentService.GetMedicamentIds(
                dto.PerscriptionDetails.Select(detail => detail.MedicamentName).ToList(), cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
        }

        return Created();
    }

    private static (bool isValid, string? errorMsg) Validate(PerscribeDto dto)
    {
        if(dto.DueDate < dto.Date)
        {
            return (false, "Perscription date cannot be after its due date");
        }

        if (dto.PerscriptionDetails.Count > 10)
        {
            return (false, "Perscription cannot include more than 10 medicaments");
        }

        return (true, null);
    }
}