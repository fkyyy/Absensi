using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Leave : BaseEntity
    {
        [Key]
        [Column("IdLeaves")]
        public Guid IdLeaves { get; set; }

        [Required]
        [Column("IdUser")]
        public Guid IdUser { get; set; }

        [Required]
        [Column("TotalLeave")]
        public int TotalLeave { get; set; }

        [Required]
        [Column("RemainingLeave")]
        public int RemainingLeave { get; set; }

        [Required]
        [Column("LeaveExpiry")]
        public DateTime LeaveExpiry { get; set; }

        [ForeignKey("IdUser")]
        public virtual User? User { get; set; }

        public Leave()
        {
            IdLeaves = Guid.NewGuid();
        }
    }
}
