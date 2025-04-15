using MediatR;
using Persistence;
using Domain;
using Application.Common.Dtos.Attachments;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Attachments.Queries;

public class GetAttachmentById
{
    public class Query : IRequest<AttachmentDto>
    {
        public Guid AttachmentId { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, AttachmentDto>
    {
        public async Task<AttachmentDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var att = await context.Attachments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AttachmentId == request.AttachmentId, cancellationToken);

            if (att == null)
                throw new Exception("Attechment not found");

            return mapper.Map<AttachmentDto>(att);
        }
    }
}
