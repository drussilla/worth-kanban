using KanbanBoard.Shared.Requests;

namespace KanbanBoard.Server.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Patch task if it exists, create new if not. This can be separated, but for the assignment purpuse I left it combined.
        /// </summary>
        Task UpdateOrCreateTask(Guid id, UpdateOrCreateTaskRequest command, CancellationToken token);

        /// <summary>
        /// Move task to a new Stage
        /// </summary>
        Task MoveTask(Guid id, Guid newStageId, CancellationToken token);

        /// <summary>
        /// Delete task from the repository.
        /// </summary>
        Task DeleteTask(Guid id, CancellationToken token);
    }
}
