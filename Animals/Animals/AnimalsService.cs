namespace Animals;

public interface IAnimalsService
{
    IList<Animal> GetAnimals(string? orderBy = null);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    void DeleteAnimal(int id);
}

public class AnimalsService(IAnimalsRepository repository) : IAnimalsService
{
    public IList<Animal> GetAnimals(string? orderBy)
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