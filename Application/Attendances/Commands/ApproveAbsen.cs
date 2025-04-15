using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Users.Commands;
using Domain;

namespace Application.Attendances.Commands;

public class ApproveAbsen
{
    public class Command : IRequest
    {
        public Guid IdAttendance { get; set; }
        public string IsApproved { get; set; } = "Approved"; // Valid: "Approved" atau "Rejected"
    }

    public class Handler(AppDbContext context, UserClaimsHelper claims) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var role = claims.GetUserRole();
            var userId = claims.GetUserId();
            var userDivision = claims.GetUserDivision();

            if (role == "Staff")
                throw new UnauthorizedAccessException("Staff tidak memiliki hak akses untuk approve absensi.");

            if (request.IsApproved != "Approved" && request.IsApproved != "Rejected")
                throw new ArgumentException("Status persetujuan tidak valid. Hanya boleh 'Approved' atau 'Rejected'.");

            var attendance = await context.Attendances
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.IdAttendance == request.IdAttendance, cancellationToken);

            if (attendance == null)
                throw new InvalidOperationException("Data absensi tidak ditemukan.");

            if (attendance.AttendanceType != EnumType.Izin && attendance.AttendanceType != EnumType.Sakit)
                throw new InvalidOperationException("Hanya absensi Izin/Sakit yang bisa di-approve.");

            if (role == "Leader" && attendance.User!.IdDivision != userDivision)
                throw new UnauthorizedAccessException("Leader hanya dapat meng-approve absensi dari divisinya sendiri.");

            attendance.IsApproved = request.IsApproved;
            attendance.ApprovedBy = userId;
            attendance.Status = request.IsApproved == "Approved" ? "Disetujui" : "Ditolak";

            await context.SaveChangesAsync(cancellationToken);
            await context.Entry(attendance).ReloadAsync(cancellationToken);
        }
    }
}
