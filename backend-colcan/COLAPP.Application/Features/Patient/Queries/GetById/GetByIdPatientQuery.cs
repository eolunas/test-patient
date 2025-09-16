using MediatR;

namespace COLAPP.Application.Features.Patient.Queries.GetById;

/// <summary>
/// Comando para obtener todos los pacientes.
/// </summary>
public record GetByIdPatientQuery(long Id) : IRequest<Domain.Entities.Patient>;
