using Microsoft.EntityFrameworkCore;
using Prescription.Context;
using Prescription.Models;

namespace Prescription.Repositories;

public interface IDoctorRepository
{
    Task<Doctor> GetDoctor(int idDoctor, CancellationToken cancellationToken);
}

public class DoctorRepository(PrescriptionAppContext context) : IDoctorRepository
{
    private readonly PrescriptionAppContext _context = context;

    public async Task<Doctor> GetDoctor(int idDoctor, CancellationToken cancellationToken)
    {
        return await _context.Doctors.SingleAsync(doctor => doctor.IdDoctor == idDoctor, cancellationToken);
    }
}