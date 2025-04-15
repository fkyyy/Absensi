using System;
using Application.Common.Dtos.Attendances;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Attendances.Commands;

public class UpdateAttendance
{
    public class Command : IRequest
    {
        public required Guid IdUser { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var existing = await context.Attendances
                .Where(a => a.IdUser == request.IdUser && a.CheckOut == null)
                .OrderByDescending(a => a.CheckIn)
                .FirstOrDefaultAsync();

            if (existing == null)
                throw new Exception("Tidak ada absensi aktif.");

            existing.CheckOut = DateTime.UtcNow;
            existing.Status = "Complete";

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
