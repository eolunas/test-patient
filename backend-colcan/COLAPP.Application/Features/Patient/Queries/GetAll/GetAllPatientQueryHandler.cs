using COLAPP.Application.Common.Interfaces;
using MediatR;

namespace COLAPP.Application.Features.Patient.Queries.GetAll;

/// <summary>
/// Handler creacion de paciente.
/// </summary>
/// <param name="patientRepo"></param>
public class GetAllPatientQueryHandler(IPatientRepository patientRepo) : IRequestHandler<GetAllPatientQuery, IEnumerable<Domain.Entities.Patient>>
{
    public readonly IPatientRepository _patientRepo = patientRepo;
    public async Task<IEnumerable<Domain.Entities.Patient>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        return await _patientRepo.GetAllAsync();
    }
}
