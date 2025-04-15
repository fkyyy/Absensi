using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Application.Common.Dtos.Attachments;

public class CreateAttachmentDto
{

    [Required]
    public IFormFile FileSource { get; set; } = default!;
}
