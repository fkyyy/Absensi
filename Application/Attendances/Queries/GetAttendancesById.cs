using System;
using Application.Common.Dtos.Attendances;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Attendances.Queries;

public class GetAttendancesById
{
    public class Query : IRequest<AttendanceDto>
    {
        public Guid IdAttendance { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, AttendanceDto>
    {
        public async Task<AttendanceDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var trs = await context.Attendances
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdAttendance == request.IdAttendance, cancellationToken);

            if (trs == null)
                throw new Exception("Attendance not found");

            return mapper.Map<AttendanceDto>(trs);
        }
    }
}
