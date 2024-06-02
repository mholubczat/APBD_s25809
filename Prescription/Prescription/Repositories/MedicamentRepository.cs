using Microsoft.EntityFrameworkCore;
using Prescription.Context;
using Prescription.Models;

namespace Prescription.Repositories;

public interface IMedicamentRepository
{
    public Task<Medicament> GetMedicament(string name, CancellationToken cancellationToken);
}

public class MedicamentRepository(PrescriptionAppContext context) : IMedicamentRepository
{
    public async Task<Medicament> GetMedicament(string name, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return await context.Medicaments.SingleAsync(medicament => medicament.Name == name, cancellationToken);
    }
}