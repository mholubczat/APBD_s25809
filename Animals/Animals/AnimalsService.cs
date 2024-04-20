namespace Animals;

public interface IAnimalsService
{
    IList<Animal> GetAnimals(string? orderBy);
    int CreateAnimal(Animal animal);
    void UpdateAnimal(Animal animal);
    void DeleteAnimal(int id);
}

public class AnimalsService
{
}