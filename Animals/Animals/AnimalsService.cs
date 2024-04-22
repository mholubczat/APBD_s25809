namespace Animals;

public interface IAnimalsService
{
    private const string DefaultOrdering = "name";
    IList<Animal> GetAnimals(string orderBy = DefaultOrdering);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    void DeleteAnimal(int id);
}

public class AnimalsService(IAnimalsRepository repository) : IAnimalsService
{
    public IList<Animal> GetAnimals(string orderBy)
    {
        return repository.GetAnimals(orderBy);
    }

    public int CreateAnimal(Animal animal)
    {
        return repository.CreateAnimal(animal);
    }

    public int UpdateAnimal(Animal animal)
    {
        return repository.UpdateAnimal(animal);
    }

    public void DeleteAnimal(int id)
    {
        repository.DeleteAnimal(id);
    }
}