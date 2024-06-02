using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services;

public interface IMedicamentService
{
    Task<Medicament> GetMedicament(string name, CancellationToken cancellationToken);
}

public class MedicamentService(IMedicamentRepository medicamentRepository) : IMedicamentService
{
    public async Task<Medicament> GetMedicament(string name, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return await medicamentRepository.GetMedicament(name, cancellationToken);
    }
}