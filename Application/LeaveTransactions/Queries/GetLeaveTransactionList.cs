using System;
using Application.Common.Dtos.LeaveTransactions;
using AutoMapper;
using Domain;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Users.Commands;

namespace Application.LeaveTransactions.Queries;

public class GetLeaveTransactionList
{
    public class Query : IRequest<List<LeaveTransactionDto>> { }

    public class Handler(AppDbContext context, UserClaimsHelper claim) : IRequestHandler<Query, List<LeaveTransactionDto>>
    {
        public async Task<List<LeaveTransactionDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var role = claim.GetUserRole();
            var userId = claim.GetUserId();
            var userDivision = claim.GetUserDivision();

            IQueryable<LeaveTransaction> query = context.LeaveTransactions;

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
                .Include(a => a.Division)
                .OrderByDescending(a => a.RequestDate)
                .Select(x => new LeaveTransactionDto
                {
                    IdTransaction = x.IdTransaction,
                    IdUser = x.IdUser,
                    RequestDate = x.RequestDate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = (LeaveStatusDto)(int)x.Status,
                    IdDivision = x.IdDivision
                })
                .ToListAsync(cancellationToken);
            return list;
        }
    }
}
