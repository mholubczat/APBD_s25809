namespace Animals;

public static class Configuration
{
    private const string BaseEndpoint = "/api/animals";

    public static IEndpointRouteBuilder RegisterEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(BaseEndpoint,
            (IAnimalsService service) => TypedResults.Ok(service.GetAnimals()));
        endpoints.MapGet(BaseEndpoint + "/{orderBy:alpha}",
            (IAnimalsService service, string? orderBy) => TypedResults.Ok(service.GetAnimals(orderBy)));
        endpoints.MapPost(BaseEndpoint,
            (IAnimalsService service, Animal animal) => TypedResults.Created("", service.CreateAnimal(animal)));
        endpoints.MapPut(BaseEndpoint + "/{id:int}", (IAnimalsService service, int id, Animal animal) =>
        {
            animal.Id = id;
            service.UpdateAnimal(animal);
            return TypedResults.NoContent();
        });
        endpoints.MapDelete(BaseEndpoint + "/{id:int}", (IAnimalsService service, int id) =>
        {
            service.DeleteAnimal(id);
            return TypedResults.NoContent();
        });

        return endpoints;
    }
}