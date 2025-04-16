using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Application.Common.Dtos.Attachments;

public class CreateAttachmentDto
{

    [Required]
    public IFormFile FileSource { get; set; } = default!;
    public string FileType { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
}
