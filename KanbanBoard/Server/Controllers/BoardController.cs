using KanbanBoard.Server.Repositories;
using KanbanBoard.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("board")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IBoardRepository _boardRepository;
        private static readonly Random _rnd = new Random();

        public BoardController(IBoardRepository boardRepository, ILogger<BoardController> logger)
        {
            _boardRepository = boardRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<BoardDto>> Get(CancellationToken token)
        {
            var boards = await _boardRepository.GetAsync(token);

            // TODO: Could be replaced with auto mapper
            return boards.Select(x => new BoardDto() { Id = x.Id, Name = x.Name });
        }

        [HttpGet("{id:guid}")]
        public async Task<BoardDto> Get(Guid id, CancellationToken token)
        {
            var board = await _boardRepository.GetAsync(id, token);
            return new BoardDto { Id = board.Id, Name = board.Name };
        }
    }
}