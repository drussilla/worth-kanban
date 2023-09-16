using KanbanBoard.Shared;

namespace KanbanBoard.Client.Services
{
    /// <summary>
    /// Wrapper around API endponts. This could be splitted in to separate services for Tasks, Boards and Stages.
    /// </summary>
    public interface IBoardService 
    {
        /// <summary>
        /// Gets list of all boards, related Stages and Tasks
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BoardDto>> GetBoardsAsync();

        /// <summary>
        /// Update task with new title and description
        /// </summary>
        Task UpdateTaskAsync(Guid id, Guid stageId, Guid boardId, string title, string description);

        /// <summary>
        /// Move task to a differect Stage
        /// </summary>        
        Task MoveTaskToStageAsync(Guid id, Guid stageId);
        
        /// <summary>
        /// Delete tast
        /// </summary>
        Task DeleteTaskAsync(Guid taskId);

        /// <summary>
        /// Deletes Stage
        /// </summary>
        Task DeleteStageAsync(Guid stageId);
    }
}
