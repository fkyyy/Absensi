using Application.Common.Dtos.Users;
using FluentValidation;

namespace Application.Validators.Users;

public class EditUserDtoValidator : AbstractValidator<EditUserDto>
{
    public EditUserDtoValidator()
    {
        RuleFor(x => x.IdUser)
            .NotEmpty().WithMessage("IdUser tidak boleh kosong.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nama harus diisi.");  
    }
}
