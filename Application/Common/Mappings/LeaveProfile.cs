using AutoMapper;
using Domain;
using Application.Common.Dtos.Leaves;

namespace Application.Common.Mappings
{
    public class LeaveProfile : Profile
    {
        public LeaveProfile()
        {
            CreateMap<Leave, LeaveDto>();
            CreateMap<CreateLeaveDto, Leave>();
        }
    }
} 