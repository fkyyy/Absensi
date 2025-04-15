using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Division : BaseEntity
    {
        [Key]
        [Column("IdDivision")]
        public Guid IdDivision { get; private set; }

        [Required]
        [Column("DivisionName")]
        public string DivisionName { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();  
        public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; } = new HashSet<LeaveTransaction>();
        public Division()
        {
            IdDivision = Guid.NewGuid();
        }
    }
}
