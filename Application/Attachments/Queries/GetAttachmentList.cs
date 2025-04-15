using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using AutoMapper.QueryableExtensions;
using Application.Common.Dtos.Attachments;
using Application.Users.Commands;
using Domain;

namespace Application.Attachments.Queries;

public class GetAttachmentList
{
    public class Query : IRequest<List<AttachmentDto>> { }

    public class Handler(AppDbContext context, IMapper mapper, UserClaimsHelper claims) : IRequestHandler<Query, List<AttachmentDto>>
    {
        public async Task<List<AttachmentDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var role = claims.GetUserRole();
            var userId = claims.GetUserId();
            var userDivision = claims.GetUserDivision();

            IQueryable<Attachment> query = context.Attachments;

            if (role == "Staff")
            {
                query = query.Where(a => a.IdUser == userId);
            }
            else if (role == "Leader")
            {
                query = query.Where(a => a.User!.IdDivision == userDivision);
            }

            var list = await query
                .OrderByDescending(a => a.CreatedAt)
                .ProjectTo<AttachmentDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return list;
        }

    }
}
