using System;

namespace Application.Common.Dtos.Attendances;

public class UpdateAttendanceDto
{
    public Guid IdAttendance { get; set; }
    public DateTimeOffset CheckOut { get; set; }
}
