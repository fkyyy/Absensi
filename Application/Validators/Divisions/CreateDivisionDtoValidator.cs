using Application.Common.Dtos.Divisions;
using FluentValidation;

namespace Application.Validators.Divisions
{
    public class CreateDivisionDtoValidator : AbstractValidator<CreateDivisionDto>
    {
        public CreateDivisionDtoValidator()
        {
            RuleFor(x => x.DivisionName)
                .NotEmpty().WithMessage("Nama divisi harus diisi.")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Nama divisi tidak boleh kosong.");
        }
    }
}