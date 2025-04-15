using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class LeaveTransaction : BaseEntity
    {
        [Key]
        [Column("IdTransaction")]
        public Guid IdTransaction { get; set; }

        [Required]
        [Column("IdUser")]
        public Guid IdUser { get; set; }

        [Required]
        [Column("RequestDate")]
        public DateTime RequestDate { get; set; }

        [Required]
        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("Status")]
        public LeaveStatus Status { get; set; }

        [Required]
        [Column("IdDivision")]
        public Guid IdDivision { get; set; }

        [ForeignKey("IdUser")]
        public virtual User? User { get; set; }

        [ForeignKey("IdDivision")]
        public virtual Division? Division { get; set; }

    }
    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected    
    }
}
