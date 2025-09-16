using COLAPP.Application.Common.Interfaces;
using MediatR;

namespace COLAPP.Application.Features.Patient.Commands.Update;

/// <summary>
/// Handler creacion de paciente.
/// </summary>
/// <param name="patientRepo"></param>
public class UpdatePatientCommandHandler(IPatientRepository patientRepo) : IRequestHandler<UpdatePatientCommand, int>
{
    public readonly IPatientRepository _patientRepo = patientRepo;
    public async Task<int> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Domain.Entities.Patient
        {
            Id = request.Id,
            DocumentType = request.DocumentType,
            DocumentNumber = request.DocumentNumber,
            Name = request.Name,
            Address = request.Address,
            BirthDate = request.BirthDate,
            Email = request.Email,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            IsActive = true,
        };

        await _patientRepo.UpdateAsync(patient);

        return 0;
    }
}
