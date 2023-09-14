using KanbanBoard.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/board/{boardId:guid}/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private static readonly Random _rnd = new Random();

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TaskDto> Get(Guid boardId)
        {
            return Enumerable.Range(1, 5).Select(index => new TaskDto
            {
                Id = Guid.NewGuid(),
                Title = "Random Task" + _rnd.NextInt64(10)
            })
            .ToArray();
        }

        [HttpGet("{id:guid}")]
        public TaskDto Get(Guid boardId, Guid id)
        {
            return new TaskDto { Id = id, Title = "Test" + _rnd.NextInt64(10) };
        }
    }
}