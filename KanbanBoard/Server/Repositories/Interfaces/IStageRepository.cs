namespace KanbanBoard.Server.Repositories.Interfaces
{
    public interface IStageRepository
    {
        /// <summary>
        /// Update stage title
        /// </summary>
        Task UpdateTitleAsync(Guid id, string title, CancellationToken token);

        /// <summary>
        /// Create new stage with <paramref name="title"/> inside board with <paramref name="boardId"/>
        /// </summary>
        Task CreateStageAsync(Guid id, Guid boardId, string title, CancellationToken token);
    }
}
