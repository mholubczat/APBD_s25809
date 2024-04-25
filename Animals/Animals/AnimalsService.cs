namespace Animals;

public interface IAnimalsService
{
    Task<IEnumerable<Animal>> GetAnimals(string? orderBy);
    Task<int> CreateAnimal(Animal animal);
    Task<int> UpdateAnimal(Animal animal);
    Task<int> DeleteAnimal(int id);
}

public class AnimalsService(IAnimalsRepository repository) : IAnimalsService
{
    private const string DefaultOrdering = "name";
    public async Task<IEnumerable<Animal>> GetAnimals(string? orderBy)
    {
        return await repository.GetAnimals(orderBy ?? DefaultOrdering);
    }

    public async Task<int> CreateAnimal(Animal animal)
    {
        return await repository.CreateAnimal(animal);
    }

    public async Task<int> UpdateAnimal(Animal animal)
    {
        return await repository.UpdateAnimal(animal);
    }

    public async Task<int> DeleteAnimal(int id)
    {
        return await repository.DeleteAnimal(id);
    }
}