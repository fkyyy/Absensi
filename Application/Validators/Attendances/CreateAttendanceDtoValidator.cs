using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.Attendances;
using Domain;
using FluentValidation;

namespace Application.Validators.Attendances
{
    public class CreateAttendanceDtoValidator : AbstractValidator<CreateAttendanceDto>
    {
        CreateAttendanceDtoValidator()
        {
            RuleFor(x => x.AttendanceType)
                .Must(status => Enum.IsDefined(typeof(EnumType), status)).WithMessage("Status tidak valid Hanya 0 (Hadir), 1 (WFH), atau 2 (WFA).");
        }
    }
}