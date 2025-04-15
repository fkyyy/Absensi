using System;
using MediatR;
using Persistence;

namespace Application.Users.Commands;

public class DeleteUser
{
    public class Command : IRequest
    {
        public Guid IdUser { get; set; }
    }

    public class Handler(AppDbContext context, UserClaimsHelper claims) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var role = claims.GetUserRole();

            if (role != "Admin")
                throw new UnauthorizedAccessException("Hanya admin yang diizinkan menghapus divisi.");

            var user = await context.Users.FindAsync(request.IdUser);

            if (user == null)
                throw new Exception("user not found");

            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
