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
        /// Update exiting task or create a new one with title and description
        /// </summary>
        Task UpdateOrCreateTaskAsync(Guid id, Guid stageId, Guid boardId, string title, string description);

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
        
        /// <summary>
        /// Update or Create Stage
        /// </summary>
        Task UpdateOrCreateStageAsync(Guid id, Guid boardId, string name);
    }
}
