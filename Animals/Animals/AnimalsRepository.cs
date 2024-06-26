using System.Data.SqlClient;

namespace Animals;

public interface IAnimalsRepository
{
    Task<IEnumerable<Animal>> GetAnimals(string orderBy);
    Task<int> CreateAnimal(Animal animal);
    Task<int> UpdateAnimal(Animal animal);
    Task<int> DeleteAnimal(int id);
}

public class AnimalsRepository : IAnimalsRepository
{
    private readonly IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        Initialize();
    }

    private void Initialize()
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.OpenAsync();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = """
                          SET NOCOUNT ON;
                          IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Animal')
                          BEGIN
                          CREATE TABLE Animal (
                              Id INT PRIMARY KEY IDENTITY,
                              Name NVARCHAR(200) NOT NULL,
                              Description NVARCHAR(200),
                              Category NVARCHAR(200) NOT NULL,
                              Area NVARCHAR(200) NOT NULL)
                          END;
                          """;

        cmd.ExecuteNonQueryAsync();

        cmd.CommandText = """
                          IF (SELECT COUNT(1) FROM Animal) = 0
                          BEGIN
                              INSERT INTO Animal (Name, Description, Category, Area)
                              VALUES
                                  ('Lion', 'The king of the jungle', 'Mammal', 'Africa'),
                                  ('Tiger', 'Ferocious big cat', 'Mammal', 'Asia'),
                                  ('Elephant', 'Gentle giant with a long trunk', 'Mammal', 'Africa'),
                                  ('Giraffe', 'Tall herbivore with a long neck', 'Mammal', 'Africa'),
                                  ('Panda', 'Cuddly bear from China', 'Mammal', 'Asia'),
                                  ('Kangaroo', 'Hopping marsupial', 'Mammal', 'Australia'),
                                  ('Penguin', 'Flightless bird from Antarctica', 'Bird', 'Antarctica'),
                                  ('Dolphin', 'Intelligent marine mammal', 'Mammal', 'Ocean'),
                                  ('Koala', 'Sleepy eucalyptus-loving marsupial', 'Mammal', 'Australia'),
                                  ('Grizzly Bear', 'Large brown bear species', 'Mammal', 'North America'),
                                  ('Crocodile', 'Large aquatic reptile', 'Reptile', 'Africa'),
                                  ('Hippo', 'Large semi-aquatic mammal', 'Mammal', 'Africa'),
                                  ('Zebra', 'Striped horse-like mammal', 'Mammal', 'Africa'),
                                  ('Polar Bear', 'Bear species native to the Arctic', 'Mammal', 'Arctic'),
                                  ('Rhino', 'Large herbivore with a horn', 'Mammal', 'Africa'),
                                  ('Ostrich', 'Large flightless bird', 'Bird', 'Africa'),
                                  ('Wolf', 'Carnivorous mammal known for its howl', 'Mammal', 'Various'),
                                  ('Cheetah', 'Fastest land animal', 'Mammal', 'Africa'),
                                  ('Gorilla', 'Large primate from Africa', 'Mammal', 'Africa'),
                                  ('Hawk', 'Bird of prey known for its sharp vision', 'Bird', 'Various')
                              END;
                          """;

        cmd.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Animal>> GetAnimals(string orderBy)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;

        var commandText = $"SELECT Id, Name, Description, Category, Area FROM Animal ORDER BY {orderBy}";
        cmd.CommandText = commandText;

        var dr = await cmd.ExecuteReaderAsync();
        var animals = new List<Animal>();

        while (await dr.ReadAsync())
        {
            var animal = new Animal
            {
                Id = (int)dr["Id"],
                Name = (string)dr["Name"],
                Description = dr["Description"] == DBNull.Value ? null : (string?)dr["Description"],
                Category = (string)dr["Category"],
                Area = (string)dr["Area"]
            };

            animals.Add(animal);
        }

        return animals;
    }

    public async Task<int> CreateAnimal(Animal animal)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@Name, @Description, @Category, @Area)";

        if (animal.Description == null)
        {
            cmd.Parameters.AddWithValue("@Description", DBNull.Value);
        }
        else
        {
            cmd.Parameters.AddWithValue("@Description", animal.Description);
        }

        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        var affectedCount = await cmd.ExecuteNonQueryAsync();
        return affectedCount;
    }

    public async Task<int> UpdateAnimal(Animal animal)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE Id = @Id";
        cmd.Parameters.AddWithValue("@Id", animal.Id);
        cmd.Parameters.AddWithValue("@Name", animal.Name);
        cmd.Parameters.AddWithValue("@Description", animal.Description);
        cmd.Parameters.AddWithValue("@Category", animal.Category);
        cmd.Parameters.AddWithValue("@Area", animal.Area);

        var affectedCount = await cmd.ExecuteNonQueryAsync();
        return affectedCount;
    }

    public async Task<int> DeleteAnimal(int id)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Animal WHERE Id = @Id";
        cmd.Parameters.AddWithValue("@Id", id);

        var affectedCount = await cmd.ExecuteNonQueryAsync();
        return affectedCount;
    }
}