using KanbanBoard.Server.Data;
using KanbanBoard.Server.Models;
using KanbanBoard.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<BoardRepository> logger;

        public BoardRepository(ApplicationDbContext context, ILogger<BoardRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Task<List<Board>> GetAsync(CancellationToken token)
        {
            logger.LogDebug("Getting all boards");
            return context
                .Boards
                .Include(x => x.Stages)
                .ThenInclude(x => x.Tasks)
                .ToListAsync(token);
        }

        public Task<Board> GetAsync(Guid id, CancellationToken token)
        {
            logger.LogDebug($"Getting board with id: {id}");
            return context
                .Boards
                .Include(x => x.Stages)
                .ThenInclude(x => x.Tasks)
                .FirstAsync(x => x.Id == id, token);
        }
    }
}
