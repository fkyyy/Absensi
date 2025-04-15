using System;
using Application.Common.Dtos.Attendances;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Attendances.Commands;

public class CreateAttendance
{
    public class Command : IRequest<string>
    {
        public required CreateAttendanceDto Attendance { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var dto = request.Attendance;

            if (dto.AttendanceType == EnumType.Izin || dto.AttendanceType == EnumType.Sakit)
                throw new InvalidOperationException("Gunakan endpoint khusus untuk absensi Izin atau Sakit.");

            var existing = await context.Attendances
                .FirstOrDefaultAsync(a => a.IdUser == dto.IdUser && a.CheckOut == null, cancellationToken);

            if (existing != null)
                throw new InvalidOperationException("User sudah melakukan absensi dan belum checkout.");

            var attendance = mapper.Map<Attendance>(dto);
            attendance.Date = dto.Date.Date;
            attendance.CheckIn = dto.CheckIn;
            attendance.Status = "Hadir";
            attendance.AttendanceType = dto.AttendanceType;
            attendance.IsApproved = "Disetujui";

            context.Attendances.Add(attendance);
            await context.SaveChangesAsync(cancellationToken);

            return attendance.IdAttendance.ToString();
        }
    }
}
