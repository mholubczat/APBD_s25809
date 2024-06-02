using Prescription.Models;
using Prescription.Repositories;

namespace Prescription.Services;

public interface IDoctorService
{
    Task<Doctor> GetDoctor(int idDoctor, CancellationToken cancellationToken);
}

public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository = doctorRepository;

    public async Task<Doctor> GetDoctor(int idDoctor, CancellationToken cancellationToken)
    {
        return await _doctorRepository.GetDoctor(idDoctor, cancellationToken);
    }
}