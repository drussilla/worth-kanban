namespace KanbanBoard.Server.Repositories.Interfaces
{
    public interface IStageRepository
    {
        /// <summary>
        /// Update stage title
        /// </summary>
        Task UpdateTitleAsync(Guid id, string title, CancellationToken token);

        /// <summary>
        /// Create new stage with <paramref name="name"/> inside board with <paramref name="boardId"/>
        /// </summary>
        Task CreateOrUpdateStageAsync(Guid id, Guid boardId, string name, CancellationToken token);
        
        /// <summary>
        /// Deletes Stage from DB
        /// </summary>
        Task DeleteStage(Guid id, CancellationToken token);
    }
}
