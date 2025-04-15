using System;
using Application.Common.Dtos.LeaveTransactions;
using MediatR;
using Persistence;

namespace Application.LeaveTransactions.Commands;

public class EditLeaveTransaction
{
    public class Command : IRequest
    {
        public required EditLeaveTransactionDto Leave { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
    
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var leaveEntity = await context.LeaveTransactions
                .FindAsync(request.Leave.IdTransaction, cancellationToken)
                    ?? throw new Exception("Transaction not found");
            leaveEntity.Status = (Domain.LeaveStatus)request.Leave.Status;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
