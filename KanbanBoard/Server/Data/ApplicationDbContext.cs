using Duende.IdentityServer.EntityFramework.Options;
using KanbanBoard.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KanbanBoard.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Do not allow to delete stage if there are tasks in it
            builder.Entity<Models.Task>()
                .HasOne(x => x.Stage)
                .WithMany(x => x.Tasks)
                .OnDelete(DeleteBehavior.Restrict);

            // Do not allow to delete board if there are tasks in it
            builder.Entity<Models.Task>()
                .HasOne(x => x.Board)
                .WithMany(x => x.Tasks)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Stage>()
                .HasOne(x => x.Board)
                .WithMany(x => x.Stages)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}