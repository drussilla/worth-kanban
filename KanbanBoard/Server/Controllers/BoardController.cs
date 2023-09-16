using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Shared;
using KanbanBoard.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> logger;
        private readonly IBoardRepository boardRepository;
        
        public BoardController(IBoardRepository boardRepository, ILogger<BoardController> logger)
        {
            this.boardRepository = boardRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<BoardDto>> Get(CancellationToken token)
        {
            var boards = await boardRepository.GetAsync(token);

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

        [HttpPost]
        public async Task<BoardDto> Create(CreateBoardRequest request, CancellationToken token)
        {
            if (!request.IsValid())
            {
                logger.LogError($"Invalid command to create board!");
                throw new ArgumentException("Command object is not valid");
            }

            var newBoard = await boardRepository.CreateAsync(request.Name, token);

            // TODO: Could be replaced with auto mapper or mapping classes
            return new BoardDto
            { 
                Id = newBoard.Id, 
                Name = newBoard.Name,
                Stages = newBoard.Stages.OrderBy(x => x.Order).Select(stage => new StageDto
                {
                    Id = stage.Id,
                    Name = stage.Name
                }).ToList()
            };
        }
    }
}