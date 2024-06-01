using Microsoft.EntityFrameworkCore;
using Prescription.Context;

namespace Prescription.Repositories;

public interface IMedicamentRepository
{
    public Task<int> GetMedicamentId(string name, CancellationToken cancellationToken);
}

public class MedicamentRepository(PrescriptionAppContext context) : IMedicamentRepository
{
    public async Task<int> GetMedicamentId(string name, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return (await context.Medicaments.SingleAsync(medicament => medicament.Name == name, cancellationToken)).IdMedicament;
    }
}