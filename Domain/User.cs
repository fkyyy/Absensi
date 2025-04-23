    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Domain
    {
        public class User : BaseEntity
        {
            [Key]
            [Column("IdUser")]
            public Guid IdUser { get; private set; }

            [Required]
            [Column("Name")]
            public string Name { get; set; } = string.Empty;

            [Required]
            [Column("Email")]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [Column("PasswordHash")]
            public string PasswordHash { get; set; } = string.Empty;

            [Required]
            [Column("Role")]
            public required string Role { get; set; }

            [Required]
            [Column("Status")]
            public required string Status { get; set; }

            [Required]
            [Column("IdDivision")]
            public Guid IdDivision { get; set; }

            [ForeignKey("IdDivision")]
            public virtual Division? Division { get; set; }

            public virtual ICollection<Leave> Leaves { get; set; } = new HashSet<Leave>();
            public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
            public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; } = new HashSet<LeaveTransaction>();
            public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();

            public User()
            {
                IdUser = Guid.NewGuid();
            }
        }
    }
