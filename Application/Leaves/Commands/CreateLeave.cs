using System;
using Application.Common.Dtos.Leaves;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Leaves.Commands;

public class CreateLeave
{
    public class Command : IRequest<string>
    {
    public required CreateLeaveDto Leave { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var existingLeave = await context.Leaves
                .FirstOrDefaultAsync(x => x.IdUser == request.Leave.IdUser, cancellationToken);

            if (existingLeave != null)
            {
                throw new InvalidOperationException("User sudah memiliki data cuti.");
            }
            var leaveEntity = mapper.Map<Leave>(request.Leave);

            context.Leaves.Add(leaveEntity);
            await context.SaveChangesAsync(cancellationToken);
            return leaveEntity.IdLeaves.ToString();
        }
    }
}
