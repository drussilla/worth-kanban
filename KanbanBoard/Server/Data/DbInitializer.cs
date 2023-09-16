using KanbanBoard.Server.Models;
using KanbanBoard.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Data
{
    public class DbInitializer : IDbIntitlizer
    {
        private readonly ApplicationDbContext context;
        private readonly IDefaultStagesProvider stagesProvider;
        private readonly ILogger<DbInitializer> logger;

        public DbInitializer(ApplicationDbContext context, IDefaultStagesProvider stagesProvider, ILogger<DbInitializer> logger)
        {
            this.context = context;
            this.stagesProvider = stagesProvider;
            this.logger = logger;
        }

        public void Initialize()
        {
            context.Database.Migrate();

            if (context.Boards.Any())
            {
                logger.LogInformation("Skip intilization since we already have boards");
                // There is already some data. skip seeding
                return;
            }

            // Always create default board, even if user deleted all boards
            var defaultBoard = new Board()
            {
                Id = Guid.NewGuid(),
                Name = "Default Board",
            };

            var stages = stagesProvider.GetDefaultStages();
            foreach (var stage in stages)
            {
                defaultBoard.Stages.Add(stage);
            }

            context.Boards.Add(defaultBoard);

            context.SaveChanges();
        }
    }
}
