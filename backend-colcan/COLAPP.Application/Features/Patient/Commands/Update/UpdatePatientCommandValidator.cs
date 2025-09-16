using FluentValidation;

namespace COLAPP.Application.Features.Patient.Commands.Update;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El Id del paciente es obligatorio y debe ser mayor que 0.");

        RuleFor(x => x.DocumentType)
            .NotEmpty().WithMessage("El tipo de documento es obligatorio.")
            .MaximumLength(50);

        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithMessage("El número de documento es obligatorio.")
            .MaximumLength(20);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe estar en el pasado.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("El correo electrónico no es válido.");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("El género es obligatorio.");

        RuleFor(x => x.Address)
            .MaximumLength(200).When(x => !string.IsNullOrWhiteSpace(x.Address));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}