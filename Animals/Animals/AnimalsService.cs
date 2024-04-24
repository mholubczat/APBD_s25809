namespace Animals;

public interface IAnimalsService
{
    IEnumerable<Animal> GetAnimals(string? orderBy);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int id);
}

public class AnimalsService(IAnimalsRepository repository) : IAnimalsService
{
    private const string DefaultOrdering = "name";
    public IEnumerable<Animal> GetAnimals(string? orderBy)
    {
        return repository.GetAnimals(orderBy ?? DefaultOrdering);
    }

    public int CreateAnimal(Animal animal)
    {
        return repository.CreateAnimal(animal);
    }

    public int UpdateAnimal(Animal animal)
    {
        return repository.UpdateAnimal(animal);
    }

    public int DeleteAnimal(int id)
    {
        return repository.DeleteAnimal(id);
    }
}