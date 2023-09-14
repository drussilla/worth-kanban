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
        private static readonly Random _rnd = new Random();

        public BoardController(ILogger<BoardController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<BoardDto> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new BoardDto
            {
                Id = Guid.NewGuid(),
                Name = "Random " + _rnd.NextInt64(10)
            })
            .ToArray();
        }

        [HttpGet("{id:guid}")]
        public BoardDto Get(Guid id)
        {
            return new BoardDto { Id = id, Name = "Test" + _rnd.NextInt64(10) };
        }
    }
}