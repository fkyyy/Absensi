using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.LeaveTransactions;
using FluentValidation;

namespace Application.Validators.LeaveTransactions
{
    public class EditLvDtoValidator : AbstractValidator<EditLeaveTransactionDto>
    {
        public EditLvDtoValidator()
        {
            RuleFor(x => x.IdTransaction)
                .NotEmpty().WithMessage("IdTransaction harus diisi.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("IdTransaction tidak valid.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status harus diisi.")
                .Must(status => Enum.IsDefined(typeof(LeaveStatus), status)).WithMessage("Status tidak valid Hanya 1 (Approve) atau 2 (Reject).");
        }
    }
}