using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.Leaves;
using FluentValidation;

namespace Application.Validators.Leaves
{
    public class CreateLeaveDtoValidator : AbstractValidator<CreateLeaveDto>
    {
        CreateLeaveDtoValidator()
        {
            RuleFor(x => x.IdUser)
                .NotEmpty().WithMessage("IdUser harus diisi.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("IdUser tidak valid.");

            RuleFor(x => x.RemainingLeave)
                .NotEmpty().WithMessage("Sisa cuti harus diisi.")
                .GreaterThan(0).WithMessage("Sisa cuti harus lebih besar dari 0.");

            RuleFor(x => x.TotalLeave)
                .NotEmpty().WithMessage("Total cuti harus diisi.")
                .GreaterThan(0).WithMessage("Total cuti harus lebih besar dari 0.");
                
            RuleFor(x => x.LeaveExpiry)
                .NotEmpty().WithMessage("Tanggal kadaluarsa cuti harus diisi.")
                .Must(date => date > DateTime.Now).WithMessage("Tanggal kadaluarsa cuti harus lebih besar dari tanggal sekarang.");
        }
    }
}