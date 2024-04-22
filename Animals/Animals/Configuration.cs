namespace Animals;

public static class Configuration
{
    private const string BaseEndpoint = "/api/animals";

    public static IEndpointRouteBuilder RegisterEndpoints(this IEndpointRouteBuilder endpoints)
    {
        MapGetAnimalEndpoint(endpoints);
        MapPostAnimalEndpoint(endpoints);
        MapPutAnimalEndpoint(endpoints);
        MapDeleteAnimalEndpoint(endpoints);

        return endpoints;
    }

    private static void MapGetAnimalEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(BaseEndpoint,
            (IAnimalsService service) => TypedResults.Ok(service.GetAnimals()));

        endpoints.MapGet(BaseEndpoint + "/{orderBy:alpha}", IResult (IAnimalsService service, string orderBy) =>
            {
                if (Validate(orderBy) == false)
                {
                    return TypedResults.BadRequest(new ArgumentOutOfRangeException(nameof(orderBy)));
                }

                return TypedResults.Ok(service.GetAnimals(orderBy));
            });
    }

    private static bool Validate(string orderBy)
    {
        return typeof(Animal)
            .GetProperties()
            .Any(property => property.Name.ToLower() == orderBy);
    }

    private static void MapPostAnimalEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(BaseEndpoint,
            (IAnimalsService service, Animal animal) => TypedResults.Created("", service.CreateAnimal(animal)));
    }

    private static void MapPutAnimalEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut(BaseEndpoint + "/{id:int}", (IAnimalsService service, int id, Animal animal) =>
        {
            animal.Id = id;
            service.UpdateAnimal(animal);
            return TypedResults.NoContent();
        });
    }

    private static void MapDeleteAnimalEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(BaseEndpoint + "/{id:int}", (IAnimalsService service, int id) =>
        {
            service.DeleteAnimal(id);
            return TypedResults.NoContent();
        });
    }
}