using Intelli.AgentPortal.Domain.Model;
using Intelli.AgentPortal.Domain.Model.Custom;
using Intelli.AgentPortal.Domain.Model.StoredProceduresOutput;
using Intelli.AgentPortal.Domain.Model.Views;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Intelli.AgentPortal.Domain.Database
{
    /// <summary>
    /// The agent portal database context.
    /// </summary>
    public partial class AgentPortalContext : CustomIdentityDbContext<AspNetUser>
    {
        /// <summary>
        /// Gets or sets the batches count.
        /// Used for batches_count stored procedure output.
        /// </summary>
        public DbSet<BatchesCount> BatchesCount { get; set; }

        public virtual DbSet<SystemUserView> SystemUsersView { get; set; }

        /// <summary>
        /// On model creating partial.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BatchesCount>(x => x.HasNoKey());
        }
    }
}
