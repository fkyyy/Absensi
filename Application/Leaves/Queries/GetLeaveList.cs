using Application.Common.Dtos.Leaves;
using AutoMapper;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Users.Commands;
using Domain;

namespace Application.Leaves.Queries;

public class GetLeaveList
{
    public class Query : IRequest<List<LeaveDto>> { }

    public class Handler(AppDbContext context, IMapper mapper, UserClaimsHelper claims) : IRequestHandler<Query, List<LeaveDto>>
    {
        public async Task<List<LeaveDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var role = claims.GetUserRole();
            var userId = claims.GetUserId();
            var userDivision = claims.GetUserDivision();

            IQueryable<Leave> query = context.Leaves;

            if (role == "Staff")
            {
                query = query.Where(a => a.IdUser == userId);
            }
            else if (role == "Leader")
            {
                query = query.Where(a => a.User!.IdDivision == userDivision);
            }

            var list = await query
                .OrderByDescending(a => a.RemainingLeave)
                .ProjectTo<LeaveDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return list;
        }
    }
}
