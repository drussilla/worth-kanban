using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IBoardRepository _boardRepository;
        
        public BoardController(IBoardRepository boardRepository, ILogger<BoardController> logger)
        {
            _boardRepository = boardRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<BoardDto>> Get(CancellationToken token)
        {
            var boards = await _boardRepository.GetAsync(token);

            // TODO: Could be replaced with auto mapper or mapping classes
            return boards.Select(board => new BoardDto
            {
                Id = board.Id,
                Name = board.Name,
                Stages = board.Stages.OrderBy(x => x.Order).Select(stage => new StageDto
                {
                    Id = stage.Id,
                    Name = stage.Name,
                    Tasks = stage.Tasks.Select(task => new TaskDto
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Description = task.Description
                    }).ToList()
                }).ToList()
            });
        }
    }
}