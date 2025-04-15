using Application.Common.Dtos.Users;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Mappings;
using Application.Users.Commands;
using Domain;

namespace Application.Users.Queries;

public class GetUserList
{
    public class Query : IRequest<List<UserDto>> { }

    public class Handler(AppDbContext context, IMapper mapper, UserClaimsHelper claims) : IRequestHandler<Query, List<UserDto>>
    {
       public async Task<List<UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var role = claims.GetUserRole();
            var userId = claims.GetUserId();
            var userDivision = claims.GetUserDivision();

            IQueryable<User> query = context.Users;

            if (role == "Staff")
            {
                query = query.Where(a => a.IdUser == userId);
            }
            else if (role == "Leader")
            {
                query = query.Where(a => a.IdDivision == userDivision);
            }

            var list = await query
                .OrderByDescending(a => a.CreatedAt)
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return list;
        }
    }
}
