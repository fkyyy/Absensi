using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.Leaves;
using FluentValidation;

namespace Application.Validators.Leaves
{
    public class EditLeaveDtoValidator : AbstractValidator<EditLeaveDto>
    {
        EditLeaveDtoValidator()
        {
            RuleFor(x => x.IdLeaves)
                .NotEmpty().WithMessage("IdLeaves harus diisi.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("IdLeaves tidak valid.");
            
            RuleFor(x => x.RemainingLeave)
                .NotEmpty().WithMessage("Sisa cuti Harus di Perbaharui")
                .GreaterThan(0).WithMessage("Sisa cuti harus lebih besar dari 0.");

            RuleFor(x => x.TotalLeave)
                .NotEmpty().WithMessage("Total cuti harus diisi.")
                .GreaterThan(0).WithMessage("Total cuti harus lebih besar dari 0.");
        }
    }
}