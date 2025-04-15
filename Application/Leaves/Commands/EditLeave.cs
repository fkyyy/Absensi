using System;
using Application.Common.Dtos.Leaves;
using MediatR;
using Persistence;

namespace Application.Leaves.Commands;

public class EditLeave
{
    public class Command : IRequest
    {
        public required EditLeaveDto Leave { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
    
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
           var userEntity = await context.Leaves
                .FindAsync([request.Leave.IdLeaves], cancellationToken)
                    ?? throw new Exception("Leave not found");
            userEntity.RemainingLeave = request.Leave.RemainingLeave;
            userEntity.TotalLeave = request.Leave.TotalLeave;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
