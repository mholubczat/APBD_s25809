using Microsoft.AspNetCore.Mvc;
using Prescription.DTOs;
using Prescription.Services;

namespace Prescription.Controllers;

[ApiController]
public class PrescriptionController(IPrescriptionService prescriptionService) : ControllerBase
{
    [Route("{idDoctor:int}/prescribe")]
    public async Task<IActionResult> Prescribe(int idDoctor, PrescribeDto dto, CancellationToken cancellationToken)
    {
        var validationResult = Validate(dto);
        if (validationResult.isValid == false)
        {
            return BadRequest(validationResult.errorMsg);
        }

        try
        {
            await prescriptionService.AddPrescription(idDoctor, dto, cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
        }

        return Created();
    }

    private static (bool isValid, string? errorMsg) Validate(PrescribeDto dto)
    {
        if(dto.DueDate < dto.Date)
        {
            return (false, "Prescription date cannot be after its due date");
        }

        if (dto.PrescriptionDetails.Count > 10)
        {
            return (false, "Prescription cannot include more than 10 medicaments");
        }

        return (true, null);
    }
}