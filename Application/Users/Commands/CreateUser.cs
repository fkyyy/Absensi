using Application.Common.Dtos;
using Application.Common.Dtos.Users;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;  

namespace Application.Users.Commands;

public class CreateUser
{
    public class Command : IRequest<string>
    {
        public required CreateUserDto User { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var userEntity = mapper.Map<User>(request.User);

            userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userEntity.PasswordHash);

            context.Users.Add(userEntity);
            await context.SaveChangesAsync(cancellationToken);

            return userEntity.IdUser.ToString();
        }
    }   
}
