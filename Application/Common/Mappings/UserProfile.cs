using AutoMapper;
using Domain;
using Application.Common.Dtos.Users;

namespace Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
} 