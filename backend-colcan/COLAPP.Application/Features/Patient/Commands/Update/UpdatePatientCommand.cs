using MediatR;

namespace COLAPP.Application.Features.Patient.Commands.Update;

/// <summary>
/// Comando para creacion de un paciente.
/// </summary>
/// <param name="DocumentType"></param>
/// <param name="DocumentNumber"></param>
/// <param name="Name"></param>
/// <param name="BirthName"></param>
/// <param name="Email"></param>
/// <param name="Gender"></param>
/// <param name="Address"></param>
/// <param name="PhoneNumber"></param>
/// <param name="IsActive"></param>
public record UpdatePatientCommand(
    long Id,
    string DocumentType,
    string DocumentNumber,
    string Name,
    DateTime BirthDate,
    string Email, 
    string Gender,
    string Address,
    string PhoneNumber) : IRequest<int>;
