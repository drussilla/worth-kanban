using KanbanBoard.Server.Data;
using KanbanBoard.Shared.Commands;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Repositories
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Patch task if it exists, create new if not. This can be separated, but for the assignment purpuse I left it combined.
        /// </summary>
        Task PatchOrCreateTask(Guid id, PatchOrCreateTaskCommand command, CancellationToken token);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(ApplicationDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task PatchOrCreateTask(Guid id, PatchOrCreateTaskCommand command, CancellationToken token)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, token);
            if (task == default)
            {
                _logger.LogInformation("Task doesn't exists. Creating new one.");
                task = new Models.Task()
                {
                    Id = id,
                    StageId = command.StageId,
                    BoardId = command.BoardId,
                    Title = command.Title,
                    Description = command.Description
                };
                _context.Tasks.Add(task);
            }
            else 
            {
                task.Title = command.Title;
                task.Description = command.Description;
            }

            await _context.SaveChangesAsync(token);
        }
    }
}
