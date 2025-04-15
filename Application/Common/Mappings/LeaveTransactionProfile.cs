using AutoMapper;
using Domain;
using Application.Common.Dtos.Leaves;
using Application.Common.Dtos.LeaveTransactions;

namespace Application.Common.Mappings
{
    public class LeaveTransactionProfile  : Profile
    {
        public LeaveTransactionProfile()
        {
            CreateMap<LeaveTransaction, LeaveTransactionDto>();
            CreateMap<CreateLeaveTransactionDto, LeaveTransaction>();
        }
    }
} 