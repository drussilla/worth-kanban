using KanbanBoard.Server.Models;

namespace KanbanBoard.Server.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        /// <summary>
        /// Create new board with a <paramref name="name"/> name.
        /// </summary>
        Task<Board> CreateAsync(string name, CancellationToken token);

        /// <summary>
        /// Get all boards with all related entities (e.g. Stages, Tasks)
        /// </summary>
        Task<List<Board>> GetAsync(CancellationToken token);

        /// <summary>
        /// Get board by id 
        /// </summary>
        Task<Board> GetAsync(Guid id, CancellationToken token);
        
        /// <summary>
        /// Update boards name
        /// </summary>
        System.Threading.Tasks.Task UpdateNameAsync(Guid id, string name, CancellationToken token);
    }
}
