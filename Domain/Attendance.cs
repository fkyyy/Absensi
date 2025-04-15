using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Attendance : BaseEntity
    {
        [Key]
        [Column("IdAttendance")]
        public Guid IdAttendance { get; set; }

        [Required]
        [Column("IdUser")]
        public Guid IdUser { get; set; }

        [Required]
        [Column("Date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("CheckIn")]
        public DateTimeOffset CheckIn { get; set; }

        [Column("CheckOut")]
        public DateTimeOffset? CheckOut { get; set; }

        [Required]
        [Column("Status")]
        public string Status { get; set; } = string.Empty;

        [Column("Notes")]
        public string Notes { get; set; } = string.Empty;

        [Column("ApprovedBy")]
        public Guid? ApprovedBy { get; set; }

        [Column("IsApproved")]
        public string IsApproved { get; set; } = string.Empty;

        [Required]
        [Column("AttendanceType")]
        public EnumType AttendanceType { get; set; } = EnumType.Hadir;

        [Column("AttachmentId")]
        public Guid? AttachmentId { get; set; }

        [ForeignKey("AttachmentId")]
        public virtual Attachment? Attachment { get; set; }

        [ForeignKey("IdUser")]
        public virtual User? User { get; set; }

        public Attendance()
        {
            IdAttendance = Guid.NewGuid();
        }
    }
}
