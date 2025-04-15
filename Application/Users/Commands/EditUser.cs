using System;
using Application.Common.Dtos.Users;
using MediatR;
using Persistence;

namespace Application.Users.Commands;

public class EditUser
{
    public class Command : IRequest
    {
        public required EditUserDto User { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
    
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var userEntity = await context.Users
                .FindAsync([request.User.IdUser], cancellationToken)
                    ?? throw new Exception("User not found");
            userEntity.Name = request.User.Name;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
