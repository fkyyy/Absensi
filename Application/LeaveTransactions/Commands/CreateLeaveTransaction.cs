using System;
using Application.Common.Dtos.LeaveTransactions;
using Application.Users.Commands;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.LeaveTransactions.Commands;

public class CreateLeaveTransaction
{
    public class Command : IRequest<string>
    {
    public required CreateLeaveTransactionDto Transaction { get; set; }
    }

   public class Handler(AppDbContext context, IMapper mapper, UserClaimsHelper claims)
    : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = claims.GetUserId();
            var userDivision = claims.GetUserDivision();

            var daysRequested = (request.Transaction.EndDate - request.Transaction.StartDate).Days + 1;
            
            var leave = await context.Leaves.FirstOrDefaultAsync(x => x.IdUser == userId, cancellationToken);
            if (leave == null)
                throw new InvalidOperationException("Data cuti user tidak ditemukan.");
            if (leave.RemainingLeave < daysRequested)
                throw new InvalidOperationException("Sisa cuti tidak mencukupi.");
    
            var leaveEntity = mapper.Map<LeaveTransaction>(request.Transaction);
            leaveEntity.IdUser = userId;
            leaveEntity.IdDivision = userDivision;

            context.LeaveTransactions.Add(leaveEntity);
            await context.SaveChangesAsync(cancellationToken);

            return leaveEntity.IdTransaction.ToString();
        }
    }
}
