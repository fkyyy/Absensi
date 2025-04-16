using System.Net.Http;
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

    public class Handler : IRequestHandler<Query, FileDownloadResult>
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public Handler(AppDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        public async Task<FileDownloadResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var attachment = await _context.Attachments.FindAsync(new object[] { request.AttachmentId }, cancellationToken);

            if (attachment == null)
                throw new FileNotFoundException("Attachment tidak ditemukan di database.");

            var fileUrl = attachment.FileSource;

            try
            {
                var response = await _httpClient.GetAsync(fileUrl, cancellationToken);
                if (!response.IsSuccessStatusCode)
                    throw new FileNotFoundException("Gagal mengunduh file dari Cloudinary.", fileUrl);

                var fileBytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
                var contentType = response.Content.Headers.ContentType?.MediaType ?? GetMimeType(fileUrl);

                return new FileDownloadResult
                {
                    FileBytes = fileBytes,
                    FileName = Path.GetFileName(fileUrl),
                    ContentType = contentType
                };
            }
            catch (Exception ex)
            {
                throw new IOException("Terjadi kesalahan saat mengunduh file dari Cloudinary.", ex);
            }
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
