using Application.Common.Dtos.Users;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users.Queries;

public class GetUserById
{
    public class Query : IRequest<UserDto>
    {
        public Guid IdUser { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, UserDto>
    {
        public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdUser == request.IdUser, cancellationToken);

            if (user == null)
                throw new Exception("User not found");

            return mapper.Map<UserDto>(user);
        }
    }
}
