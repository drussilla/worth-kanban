using KanbanBoard.Server.Data;
using KanbanBoard.Server.Models;
using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IDefaultStagesProvider defaultStagesProvider;
        private readonly ILogger<BoardRepository> logger;

        public BoardRepository(ApplicationDbContext context, IDefaultStagesProvider defaultStagesProvider, ILogger<BoardRepository> logger)
        {
            this.context = context;
            this.defaultStagesProvider = defaultStagesProvider;
            this.logger = logger;
        }

        public async Task<Board> CreateAsync(string name, CancellationToken token)
        {
            var board = new Board
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            var stages = defaultStagesProvider.GetDefaultStages();
            foreach (var stage in stages)
            {
                board.Stages.Add(stage);
            }

            context.Boards.Add(board);
            await context.SaveChangesAsync(token);
            return board;
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

        public async System.Threading.Tasks.Task UpdateNameAsync(Guid id, string name, CancellationToken token)
        {
            var board = await context.Boards.FirstAsync(x => x.Id == id, token);
            board.Name = name;
            await context.SaveChangesAsync(token);
        }
    }
}
