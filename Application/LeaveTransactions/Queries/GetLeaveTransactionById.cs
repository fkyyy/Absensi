using System;
using Application.Common.Dtos.LeaveTransactions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.LeaveTransactions.Queries;

public class GetLeaveTransactionById
{
    public class Query : IRequest<LeaveTransactionDto>
    {
        public Guid IdTransaction { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, LeaveTransactionDto>
    {
        public async Task<LeaveTransactionDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var trs = await context.LeaveTransactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdTransaction == request.IdTransaction, cancellationToken);

            if (trs == null)
                throw new Exception("User not found");

            return mapper.Map<LeaveTransactionDto>(trs);
        }
    }
}
