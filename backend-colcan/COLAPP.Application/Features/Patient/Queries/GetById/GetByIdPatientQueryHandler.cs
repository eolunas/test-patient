using COLAPP.Application.Common.Interfaces;
using MediatR;

namespace COLAPP.Application.Features.Patient.Queries.GetById;

/// <summary>
/// Handler creacion de paciente.
/// </summary>
/// <param name="patientRepo"></param>
public class GetByIdPatientQueryHandler(IPatientRepository patientRepo) : IRequestHandler<GetByIdPatientQuery, Domain.Entities.Patient>
{
    public readonly IPatientRepository _patientRepo = patientRepo;
    public async Task<Domain.Entities.Patient> Handle(GetByIdPatientQuery request, CancellationToken cancellationToken)
    {
        return await _patientRepo.GetByIdAsync(request.Id);
    }
}
