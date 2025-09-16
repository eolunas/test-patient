using COLAPP.Application.Common.Interfaces;
using COLAPP.Domain.Entities;
using COLAPP.Infrastructure.Persistence;
using COLAPP.Shared.DependencyInjection;
using Dapper;

namespace COLAPP.Infrastructure.Repositories;

/// <summary>
/// Implementacion de repositorio de pascientes.
/// </summary>
/// <param name="context"></param>
[Scoped]
public class PatientRepository(DapperContext context) : IPatientRepository
{
    private readonly DapperContext _context = context;

    public async Task<Patient?> GetByIdAsync(long id)
    {
        using var connection = _context.CreateConection();
        var patient = await connection.QueryFirstOrDefaultAsync<Patient>(
            "sp_GetByIdAsync",
            new { Id = id },
            commandType: System.Data.CommandType.StoredProcedure
            );

        return patient;
    }

    public async Task<IEnumerable<Patient?>> GetAllAsync()
    {
        using var connection = _context.CreateConection();
        var patient = await connection.QueryAsync<Patient>(
            "sp_GetAllAsync",
            commandType: System.Data.CommandType.StoredProcedure
            );

        return patient;
    }

    public async Task<int> AddAsync(Patient patient)
    {
        using var connection = _context.CreateConection();
        var id = await connection.ExecuteScalarAsync<int>(
            "sp_CreatePatient",
            new
            {
                patient.DocumentType,
                patient.DocumentNumber,
                patient.Name,
                patient.BirthDate,
                patient.Email,
                patient.Gender,
                patient.Address,
                patient.PhoneNumber,
                patient.IsActive,
            },
            commandType: System.Data.CommandType.StoredProcedure
            );

        return id;
    }

    public async Task UpdateAsync(Patient patient)
    {
        using var connection = _context.CreateConection();
        await connection.ExecuteAsync(
            "sp_UpdatePatient",
            new
            {
                patient.Id,
                patient.DocumentType,
                patient.DocumentNumber,
                patient.Name,
                patient.BirthDate,
                patient.Email,
                patient.Gender,
                patient.Address,
                patient.PhoneNumber,
                patient.IsActive,
            },
            commandType: System.Data.CommandType.StoredProcedure
            );
    }

    public async Task DeleteAsync(long id)
    {
        using var connection = _context.CreateConection();
        await connection.ExecuteAsync(
            "sp_DeletePatient",
            new { Id = id } ,
            commandType: System.Data.CommandType.StoredProcedure
            );
    }

}
