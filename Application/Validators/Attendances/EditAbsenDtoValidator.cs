using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.Attendances;
using FluentValidation;

namespace Application.Validators.Attendances
{
    public class EditAbsenDtoValidator : AbstractValidator<ApproveAbsenDto>
    {
        public EditAbsenDtoValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status harus diisi.")
                .Must(status => "Approve" == status || "Reject" == status).WithMessage("Status tidak valid. Hanya 'Approve' atau 'Reject' yang diperbolehkan.");
        }
    }
}