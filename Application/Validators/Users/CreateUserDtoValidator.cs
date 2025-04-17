using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.Users;
using FluentValidation;

namespace Application.Validators.Users
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama harus diisi.");
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email harus diisi.")
                .EmailAddress().WithMessage("Format email tidak valid.");
            
            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Password harus diisi.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role harus diisi.")
                .Must(role => new[] { "Admin", "User" }.Contains(role)).WithMessage("Role tidak valid.");
            
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status harus diisi.")
                .Must(status => new[] { "Active", "Inactive" }.Contains(status)).WithMessage("Status tidak valid.");
            
            RuleFor(x => x.IdDivision)
                .NotEmpty().WithMessage("IdDivision harus diisi.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("IdDivision tidak valid.");
        }
    }
}