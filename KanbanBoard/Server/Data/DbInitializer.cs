using KanbanBoard.Server.Models;

namespace KanbanBoard.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

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
                Name = "Backlogs"
            });

            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Planned"
            });

            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "In Progress"
            });

            defaultBoard.Stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Completed"
            });

            context.Boards.Add(defaultBoard);

            context.SaveChanges();
        }
    }
}
