using System;
using Domain;

namespace Application.Common.Dtos.Attendances
{
    public class AttendanceDto
    {
        public Guid IdAttendance { get; set; }
        public Guid IdUser { get; set; }
        public DateTime Date { get; set; }
        public DateTimeOffset CheckIn { get; set; }
        public DateTimeOffset? CheckOut { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public Guid? ApprovedBy { get; set; }
        public string IsApproved { get; set; } = "Pending";

        public EnumType AttendanceType { get; set; } = EnumType.Hadir;
        public Guid? AttachmentId { get; set; }
    }
}
