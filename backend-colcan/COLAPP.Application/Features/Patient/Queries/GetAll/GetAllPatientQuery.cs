using MediatR;

namespace COLAPP.Application.Features.Patient.Queries.GetAll;

/// <summary>
/// Comando para obtener todos los pacientes.
/// </summary>
public record GetAllPatientQuery() : IRequest<IEnumerable<Domain.Entities.Patient>>;
