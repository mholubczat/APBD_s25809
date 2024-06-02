using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services;

public interface IMedicamentService
{
    Task<Medicament> GetMedicament(string name, CancellationToken cancellationToken);
}

public class MedicamentService(IMedicamentRepository medicamentRepository) : IMedicamentService
{
    private readonly IMedicamentRepository _medicamentRepository = medicamentRepository;

    public async Task<Medicament> GetMedicament(string name, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return await _medicamentRepository.GetMedicament(name, cancellationToken);
    }
}