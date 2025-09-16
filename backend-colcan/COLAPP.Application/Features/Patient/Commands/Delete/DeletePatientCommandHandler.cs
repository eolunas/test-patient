using COLAPP.Application.Common.Interfaces;
using MediatR;

namespace COLAPP.Application.Features.Patient.Commands.Delete;

/// <summary>
/// Handler creacion de paciente.
/// </summary>
/// <param name="patientRepo"></param>
public class DeletePatientCommandHandler(IPatientRepository patientRepo) : IRequestHandler<DeletePatientCommand, int>
{
    public readonly IPatientRepository _patientRepo = patientRepo;
    public async Task<int> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        await _patientRepo.DeleteAsync(request.Id);

        return 0;
    }
}
