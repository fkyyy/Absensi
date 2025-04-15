using Application.Common.Dtos.Attachments;
using Application.Users.Commands;
using Domain;
using MediatR;
using Persistence;

namespace Application.Attachments.Commands
{
    public class CreateAttachment
    {
        public class Command : IRequest<string>
        {
            public required CreateAttachmentDto Attachment { get; set; }
        }

        public class Handler(AppDbContext context, UserClaimsHelper claims) : IRequestHandler<Command, string>
        {
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = claims.GetUserId();

                var dto = request.Attachment;
                var fileExtension = Path.GetExtension(dto.FileSource.FileName)?.ToLowerInvariant().TrimStart('.');

                var attEntity = new Attachment
                {
                    AttachmentId = Guid.NewGuid(),
                    IdUser = userId,
                    FileType = fileExtension ?? "unknown",
                    FileSource = dto.FileSource.FileName
                };
                
                context.Attachments.Add(attEntity);
                await context.SaveChangesAsync(cancellationToken);

                return attEntity.AttachmentId.ToString();
            }
        }
    }
}
