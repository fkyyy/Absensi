using Application.Common.Dtos.Attendances;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Attendances.Commands;

public class CreateAbsen
{
    public class Command : IRequest<string>
    {
        public required AbsenDto Attendance { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var dto = request.Attendance;

            if (dto.AttendanceType != EnumType.Izin && dto.AttendanceType != EnumType.Sakit)
                throw new InvalidOperationException("AttendanceType harus Izin atau Sakit untuk endpoint ini.");

            if (dto.AttachmentId == null)
                throw new ArgumentException("Lampiran harus disertakan untuk izin/sakit.");

            var attachmentValid = await context.Attachments
                .AnyAsync(a => a.AttachmentId == dto.AttachmentId && a.IdUser == dto.IdUser, cancellationToken);

            if (!attachmentValid)
                throw new ArgumentException("Lampiran tidak valid atau bukan milik user.");

            var attendance = mapper.Map<Attendance>(dto);
            attendance.Date = dto.Date.Date;
            attendance.CheckIn = attendance.CheckIn;
            attendance.CheckOut = attendance.CheckOut;
            attendance.Status = "Menunggu Persetujuan";
            attendance.IsApproved = "Pending";
            attendance.AttendanceType = dto.AttendanceType;
            attendance.ApprovedBy = null;

            context.Attendances.Add(attendance);
            await context.SaveChangesAsync(cancellationToken);

            return attendance.IdAttendance.ToString();
        }
    }
}