using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos.Attendances;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Attendances.Commands
{
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

                // Validation for AttendanceType (you can use FluentValidation for better structure)
                if (dto.AttendanceType == EnumType.Izin || dto.AttendanceType == EnumType.Sakit)
                    throw new ArgumentException("Use the dedicated endpoint for Izin or Sakit attendance.");

                // Check if the user already has an active attendance
                var existing = await context.Attendances
                    .FirstOrDefaultAsync(a => a.IdUser == dto.IdUser && a.CheckOut == null, cancellationToken);

                if (existing != null)
                    throw new InvalidOperationException("User already checked in and hasn't checked out yet.");

                // Map DTO to Entity
                var attendance = mapper.Map<Attendance>(dto);
                attendance.Date = dto.Date.Date;
                attendance.CheckIn = dto.CheckIn;
                attendance.Status = "Hadir";
                attendance.AttendanceType = dto.AttendanceType;
                attendance.IsApproved = "Disetujui"; // Default approval status can be dynamic based on business logic

                // Add the new attendance record to the database
                context.Attendances.Add(attendance);
                await context.SaveChangesAsync(cancellationToken);

                // Return the attendance ID as a string
                return attendance.IdAttendance.ToString();
            }
        }
    }
}
