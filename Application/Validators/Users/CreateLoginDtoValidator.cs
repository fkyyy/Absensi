using System;
using Application.Common.Dtos.Users;
using Application.Users.Commands;
using FluentValidation;

namespace Application.Validators.Users;

public class CreateLoginDtoValidator : AbstractValidator<LoginDto>
{
     public CreateLoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email harus diisi.")
            .EmailAddress().WithMessage("Format email tidak valid.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password harus diisi.");

    }
}
