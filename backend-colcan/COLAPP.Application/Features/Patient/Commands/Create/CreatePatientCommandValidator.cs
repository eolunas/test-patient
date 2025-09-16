using FluentValidation;

namespace COLAPP.Application.Features.Patient.Commands.Create;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.DocumentType)
            .NotEmpty().WithMessage("El tipo de documento es obligatorio")
            .MaximumLength(50);

        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithMessage("El número de documento es obligatorio")
            .MaximumLength(20);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100);

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe ser en el pasado");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("El género es obligatorio");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(50);
    }
}