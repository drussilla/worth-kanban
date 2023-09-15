using KanbanBoard.Server.Repositories;
using KanbanBoard.Shared;
using KanbanBoard.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<TaskController> logger;
        private static readonly Random _rnd = new Random();

        public TaskController(ITaskRepository taskRepository, ILogger<TaskController> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        [HttpPatch("{id:guid}")]
        public async Task Patch(Guid id, PatchOrCreateTaskCommand command, CancellationToken token)
        {
            if (!command.IsValid())
            {
                throw new ArgumentException("Patch object is not valid");
            }

            await taskRepository.PatchOrCreateTask(id, command, token);
        }
    }
}