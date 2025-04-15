using System;
using Application.Common.Dtos.Leaves;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Leaves.Queries;

public class GetLeaveById
{
    public class Query : IRequest<LeaveDto>
    {
        public Guid IdLeaves { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, LeaveDto>
    {
        public async Task<LeaveDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var leave = await context.Leaves
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdLeaves == request.IdLeaves, cancellationToken);

            if (leave == null)
                throw new Exception("User not found");

            return mapper.Map<LeaveDto>(leave);
        }
    }
}
