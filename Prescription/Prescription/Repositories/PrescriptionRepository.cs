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
    private readonly PrescriptionAppContext _context = context;

    public async Task AddPrescription(Models.Prescription prescription, CancellationToken cancellationToken)
    {
        await _context.Prescriptions.AddAsync(prescription, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task AddPrescriptionMedicament(PrescriptionMedicament prescriptionMedicament, CancellationToken cancellationToken)
    {
        await _context.PrescriptionMedicaments.AddAsync(prescriptionMedicament, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}