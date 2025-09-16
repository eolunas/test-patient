using COLAPP.Application.Common.Interfaces;
using MediatR;

namespace COLAPP.Application.Features.Patient.Commands.Create;

/// <summary>
/// Handler creacion de paciente.
/// </summary>
/// <param name="patientRepo"></param>
public class CreatePatientCommandHandler(IPatientRepository patientRepo) : IRequestHandler<CreatePatientCommand, int>
{
    public readonly IPatientRepository _patientRepo = patientRepo;
    public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Domain.Entities.Patient
        {
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

        return await _patientRepo.AddAsync(patient);
    }
}
