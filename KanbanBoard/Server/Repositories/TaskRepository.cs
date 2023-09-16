using KanbanBoard.Server.Data;
using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<TaskRepository> logger;

        public TaskRepository(ApplicationDbContext context, ILogger<TaskRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task DeleteTask(Guid id, CancellationToken token)
        {
            // I relly on DB contexts to thorw an error if task is not in the repo. This logic can be moved one lever higher for a property handling.
            var task = await context.Tasks.FirstAsync(t => t.Id == id, token);
            context.Tasks.Remove(task);
            await context.SaveChangesAsync(token);
        }

        public async Task MoveTask(Guid id, Guid newStageId, CancellationToken token)
        {
            var task = await context.Tasks.FirstAsync(t => t.Id == id, token);
            task.StageId = newStageId; 
            await context.SaveChangesAsync(token);
        }

        public async Task UpdateOrCreateTask(Guid id, UpdateOrCreateTaskRequest command, CancellationToken token)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == id, token);
            if (task == default)
            {
                logger.LogInformation("Task doesn't exists. Creating new one.");
                task = new Models.Task()
                {
                    Id = id,
                    StageId = command.StageId,
                    BoardId = command.BoardId,
                    Title = command.Title,
                    Description = command.Description
                };
                context.Tasks.Add(task);
            }
            else 
            {
                task.Title = command.Title;
                task.Description = command.Description;
            }

            await context.SaveChangesAsync(token);
        }
    }
}
