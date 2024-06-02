using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services;

public interface IDoctorService
{
    Task<Doctor> GetDoctor(int idDoctor, CancellationToken cancellationToken);
}

public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
{
    public async Task<Doctor> GetDoctor(int idDoctor, CancellationToken cancellationToken)
    {
        return await doctorRepository.GetDoctor(idDoctor, cancellationToken);
    }
}