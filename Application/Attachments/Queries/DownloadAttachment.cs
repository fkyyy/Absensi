using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Attachments.Queries;

public class DownloadAttachment
{
    public class Query : IRequest<FileDownloadResult>
    {
        public Guid AttachmentId { get; set; }
    }

    public class FileDownloadResult
    {
        public byte[] FileBytes { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = "application/octet-stream";
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, FileDownloadResult>
    {
        public async Task<FileDownloadResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var attachment = await context.Attachments.FindAsync(new object[] { request.AttachmentId }, cancellationToken);

            if (attachment == null)
                throw new FileNotFoundException("Attachment tidak ditemukan di database.");

            var filePath = Path.Combine("C:\\Users\\IT CLASS\\Fikri\\Absensis\\Application\\Common\\FileUpload", attachment.FileSource);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File tidak ditemukan di server.", filePath);

            var fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

            return new FileDownloadResult
            {
                FileBytes = fileBytes,
                FileName = attachment.FileSource,
                ContentType = GetMimeType(attachment.FileSource)
            };
        }

        private string GetMimeType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".pdf" => "application/pdf",
                ".doc" or ".docx" => "application/msword",
                ".xls" or ".xlsx" => "application/vnd.ms-excel",
                _ => "application/octet-stream"
            };
        }
    }
}
