using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.LeaveTransactions;
using FluentValidation;

namespace Application.Validators.LeaveTransactions
{
    public class CreateLvDtoValidator : AbstractValidator<CreateLeaveTransactionDto>
    {
       public CreateLvDtoValidator()
       {
            RuleFor(x => x.RequestDate)
                .NotEmpty().WithMessage("Tanggal permohonan harus diisi.")
                .Must(date => date <= DateTime.Now).WithMessage("Tanggal permohonan tidak boleh lebih dari hari ini.");
            
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Tanggal mulai harus diisi.")
                .Must(date => date >= DateTime.Now).WithMessage("Tanggal mulai tidak boleh kurang dari hari ini.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Tanggal akhir harus diisi.")
                .Must((dto, endDate) => endDate >= dto.StartDate).WithMessage("Tanggal akhir tidak boleh kurang dari tanggal mulai.");
       }
    }
}