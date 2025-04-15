using Application.Common.Dtos.Attendances;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using AutoMapper.QueryableExtensions;
using Application.Users.Commands;

namespace Application.Attendances.Queries;

public class GetAttendancesList
{
    public class Query : IRequest<List<AttendanceDto>> { }

    public class Handler(AppDbContext context, IMapper mapper, UserClaimsHelper claims) : IRequestHandler<Query, List<AttendanceDto>>
    {
        public async Task<List<AttendanceDto>> Handle(Query request, CancellationToken cancellationToken)
        {

            var role = claims.GetUserRole();
            var userId = claims.GetUserId();
            var userDivision = claims.GetUserDivision();

            IQueryable<Attendance> query = context.Attendances;

            if (role == "Staff")
            {
                query = query.Where(a => a.IdUser == userId);
            }
            else if (role == "Leader")
            {
                query = query.Where(a => a.User!.IdDivision == userDivision);
            }

            var list = await query
                .Include(a => a.User)
                .OrderByDescending(a => a.Date)
                .ProjectTo<AttendanceDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return list;
        }
    }
}
