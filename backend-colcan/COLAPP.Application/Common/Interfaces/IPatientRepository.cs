using COLAPP.Domain.Entities;

namespace COLAPP.Application.Common.Interfaces;

/// <summary>
/// Contratos para repositorio de paciente.
/// </summary>
public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(long id);
    Task<IEnumerable<Patient?>> GetAllAsync();
    Task<int> AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(long id);
}
