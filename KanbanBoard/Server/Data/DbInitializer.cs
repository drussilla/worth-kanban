using KanbanBoard.Server.Models;
using KanbanBoard.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Data
{
    public interface IDbIntitlizer
    {
        void Initialize();
    }

    public class DbInitializer : IDbIntitlizer
    {
        private readonly ApplicationDbContext context;
        private readonly IStageOrderGenerator orderGenerator;
        private readonly ILogger<DbInitializer> logger;

        public DbInitializer(ApplicationDbContext context, IStageOrderGenerator orderGenerator, ILogger<DbInitializer> logger)
        {
            this.context = context;
            this.orderGenerator = orderGenerator;
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

            var order = orderGenerator.GenerateIntialOrder();
            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Backlogs",
                Order = order,
            });

            order = orderGenerator.GenerateOrder(order);
            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Planned",
                Order = order
            });

            order = orderGenerator.GenerateOrder(order);
            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "In Progress",
                Order = order
            });

            order = orderGenerator.GenerateOrder(order);
            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Completed",
                Order = order
            });

            context.Boards.Add(defaultBoard);

            context.SaveChanges();
        }
    }
}
