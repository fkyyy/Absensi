using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.Attendances;
using Domain;
using FluentValidation;

namespace Application.Validators.Attendances
{
    public class CreateAbsenDtoValidator : AbstractValidator<AbsenDto>
    {
        public CreateAbsenDtoValidator()
        {
            RuleFor(x => x.AttendanceType)
                .Must(AttendanceType => Enum.IsDefined(typeof(EnumType), AttendanceType))
                .WithMessage("Status tidak valid. Hanya 0 (Hadir), 1 (WFH), atau 2 (WFA).");

            RuleFor(x => x.IdUser)
                .NotEmpty().WithMessage("IdUser is required.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.");

            RuleFor(x => x.CheckIn)
                .NotEmpty().WithMessage("CheckIn is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(x => x.IsApproved)
                .NotEmpty().WithMessage("IsApproved is required.");
        }
    }
}