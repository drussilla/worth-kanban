using KanbanBoard.Server.Models;

namespace KanbanBoard.Server.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        /// <summary>
        /// Get all boards with all related entities (e.g. Stages, Tasks)
        /// </summary>
        Task<List<Board>> GetAsync(CancellationToken token);

        /// <summary>
        /// Get board by id 
        /// </summary>
        Task<Board> GetAsync(Guid id, CancellationToken token);
    }
}
