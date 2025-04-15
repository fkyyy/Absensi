using System;

namespace Application.Common.Dtos.Attachments;

 public class AttachmentDto
{
    public Guid AttachmentId { get; set; }
    public Guid IdUser { get; set; }
    public string FileType { get; set; } = string.Empty;
    public string FileSource { get; set; } = string.Empty;
}