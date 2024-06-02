using Prescription.Context;
using Prescription.Models;

namespace Prescription.Repositories;

public interface IPrescriptionRepository
{
    Task AddPrescription(Models.Prescription prescription, CancellationToken cancellationToken);
    Task AddPrescriptionMedicament(PrescriptionMedicament prescriptionMedicament, CancellationToken cancellationToken);
}

public class PrescriptionRepository(PrescriptionAppContext context) : IPrescriptionRepository
{
    public async Task AddPrescription(Models.Prescription prescription, CancellationToken cancellationToken)
    {
        await context.Prescriptions.AddAsync(prescription, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task AddPrescriptionMedicament(PrescriptionMedicament prescriptionMedicament, CancellationToken cancellationToken)
    {
        await context.PrescriptionMedicaments.AddAsync(prescriptionMedicament, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}