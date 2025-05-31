using Intelli.AgentPortal.Domain.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Intelli.AgentPortal.Domain.Database
{
    public partial class AgentPortalAuditContext : DbContext
    {
        public AgentPortalAuditContext()
        {
        }

        public AgentPortalAuditContext(DbContextOptions<AgentPortalAuditContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.AuditType)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.AuditUser)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ChangedColumns).IsRequired();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.KeyValues).IsRequired();

                entity.Property(e => e.NewValues).IsRequired();

                entity.Property(e => e.OldValues).IsRequired();

                entity.Property(e => e.TableName).IsRequired();

                entity.Property(e => e.RequestId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
