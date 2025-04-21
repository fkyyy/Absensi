using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Division> Divisions { get; set; } = default!;
        public DbSet<Leave> Leaves { get; set; } = default!;
        public DbSet<Attachment> Attachments { get; set; } = default!;
        public DbSet<LeaveTransaction> LeaveTransactions { get; set; } = default!;
        public DbSet<Attendance> Attendances { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leave>()
                .HasIndex(l => l.IdUser)
                .IsUnique();    
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Attachment)
                .WithOne()
                .HasForeignKey<Attendance>(a => a.AttachmentId)
                .OnDelete(DeleteBehavior.SetNull);
                base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Attachment>()
                .HasOne<Attendance>()
                .WithMany()
                .HasForeignKey("IdAttendance")
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
            modelBuilder.Entity<Attendance>()
                .Property(e => e.AttendanceType)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnumType)Enum.Parse(typeof(EnumType), v));

        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
