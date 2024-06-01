using Prescription.Repositories;

namespace Prescription.Services;

public interface IMedicamentService
{
    public Task<ICollection<int>> GetMedicamentIds(ICollection<string> medicaments, CancellationToken cancellationToken);
}

public class MedicamentService(IMedicamentRepository medicamentRepository) : IMedicamentService
{
    public async Task<ICollection<int>> GetMedicamentIds(ICollection<string> medicaments, CancellationToken cancellationToken)
    {
        if (medicaments == null || medicaments.Count == 0)
        {
            throw new ArgumentNullException(nameof(medicaments), "No medicaments to retrieve passed");
        }

        List<int> result = [];
        foreach (var medicament in medicaments)
        {
            result.Add(await medicamentRepository.GetMedicamentId(medicament, cancellationToken));
        }

        return result;
    }
}