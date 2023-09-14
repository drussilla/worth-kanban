using KanbanBoard.Server.Data;
using KanbanBoard.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Repositories
{
    public interface IBoardRepository 
    {
        Task<List<Board>> GetAsync(CancellationToken token);
        Task<Board> GetAsync(Guid id, CancellationToken token);
    }

    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BoardRepository> _logger;

        public BoardRepository(ApplicationDbContext context, ILogger<BoardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<List<Board>> GetAsync(CancellationToken token)
        {
            _logger.LogDebug("Getting all boards");
            return _context
                .Boards
                .Include(x => x.Stages)
                .ThenInclude(x => x.Tasks)
                .ToListAsync(token);
        }

        public Task<Board> GetAsync(Guid id, CancellationToken token)
        {
            _logger.LogDebug($"Getting board with id: {id}");
            return _context
                .Boards
                .FirstAsync(x => x.Id == id, token);
        }
    }
}
