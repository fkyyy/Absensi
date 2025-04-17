using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos.Attachments;
using FluentValidation;

namespace Application.Validators.Attachments
{
    public class CreateAttachmentDtoValidator : AbstractValidator<CreateAttachmentDto>
    {
        public CreateAttachmentDtoValidator()
        {
            RuleFor(x => x.FileSource)
                .NotEmpty().WithMessage("FileSource harus diisi.")
                .Must(file => file.Length > 0).WithMessage("FileSource tidak boleh kosong.");
        }
    }
}