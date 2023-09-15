using KanbanBoard.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (context.Boards.Any())
            {
                // There is already some data. skip seeding
                return;
            }

            // Always create default board, even if user deleted all boards
            var defaultBoard = new Board()
            {
                Id = Guid.NewGuid(),
                Name = "Default Board",
            };
            
            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Backlogs",
                Order = 100000
            });

            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Planned",
                Order = 200000
            });

            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "In Progress",
                Order = 300000
            });

            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Completed",
                Order = 400000
            });

            context.Boards.Add(defaultBoard);

            context.SaveChanges();
        }
    }
}
