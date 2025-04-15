using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Attachment : BaseEntity
    {
        [Key]
        [Column("AttachmentId")]
        public Guid AttachmentId { get; set; }

        [Required]
        [Column("IdUser")]
        public Guid IdUser { get; set; }


        [Column("FileType")]
        public string FileType { get; set; } = string.Empty;

        [Required]
        [Column("FileSource")]
        public string FileSource { get; set; } = string.Empty;

        [ForeignKey("IdUser")]
        public virtual User? User { get; set; }


        public Attachment()
        {
            AttachmentId = Guid.NewGuid();
        }
    }
}