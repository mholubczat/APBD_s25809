using Microsoft.AspNetCore.Mvc;

namespace Animals;

[Route("/api/animals")]
[ApiController]
public class AnimalsController(IAnimalsService animalsService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAnimals(string? orderBy)
    {
        var validationOutcome = Validate(orderBy);
        if (validationOutcome.isValid == false)
        {
            return BadRequest(validationOutcome.reason);
        }

        var animals = animalsService.GetAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        animalsService.CreateAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        var validationOutcome = Validate(id);
        if (validationOutcome.isValid == false)
        {
            return BadRequest(validationOutcome.reason);
        }

        animal.Id = id;
        var affectedRows = animalsService.UpdateAnimal(animal);

        if (affectedRows == 0)
        {
            return NotFound(GetNotFoundMessage(id));
        }

        return NoContent();
    }

    [HttpDelete("id:int")]
    public IActionResult DeleteAnimal(int id)
    {
        var validationOutcome = Validate(id);
        if (validationOutcome.isValid == false)
        {
            return BadRequest(validationOutcome.reason);
        }

        var affectedRows = animalsService.DeleteAnimal(id);

        if (affectedRows == 0)
        {
            return NotFound(GetNotFoundMessage(id));
        }

        animalsService.DeleteAnimal(id);
        return NoContent();
    }

    private static string GetNotFoundMessage(int id)
    {
        return $"The animal with specified id {id} has not been found";
    }

    private static (bool isValid, string reason) Validate(string? orderBy)
    {
        var isValid = orderBy == null
                      || typeof(Animal)
                          .GetProperties()
                          .Any(property => property.Name.ToLower() == orderBy);
        var reason = isValid ? string.Empty : $"Invalid input, cannot order by {orderBy}";

        return (isValid, reason);
    }

    private static (bool isValid, string reason) Validate(int id)
    {
        var isValid = id > 0;
        var reason = isValid ? string.Empty : $"Invalid input {id}, id has to be larger than zero";

        return (isValid, reason);
    }
}