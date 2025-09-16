using MediatR;

namespace COLAPP.Application.Features.Patient.Commands.Delete;

/// <summary>
/// Comando para creacion de un paciente.
/// </summary>
public record DeletePatientCommand(long Id) : IRequest<int>;
