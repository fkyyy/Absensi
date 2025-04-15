using System;
using Application.Users.Commands;
using MediatR;
using Persistence;

namespace Application.Divisions.Commands;

public class DeleteDivision
{
    public class Command : IRequest
    {
        public Guid IdDivision { get; set; }
    }

    public class Handler(AppDbContext context, UserClaimsHelper claims) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var role = claims.GetUserRole();

            if (role != "Admin")
                throw new UnauthorizedAccessException("Hanya admin yang diizinkan menghapus divisi.");

            var division = await context.Divisions.FindAsync(request.IdDivision);

            if (division == null)
                throw new Exception("Division not found");

            context.Divisions.Remove(division);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
