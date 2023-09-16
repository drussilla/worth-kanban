using KanbanBoard.Server.Repositories.Interfaces;
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

        public TaskController(ITaskRepository taskRepository, ILogger<TaskController> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        [HttpPatch("{id:guid}")]
        public async Task UpdateOrCreate(Guid id, UpdateOrCreateTaskCommand command, CancellationToken token)
        {
            if (!command.IsValid())
            {
                logger.LogError($"Invalid command to update task {id}!");
                throw new ArgumentException("Command object is not valid");
            }

            await taskRepository.UpdateOrCreateTask(id, command, token);
        }

        [HttpPost("{id:guid}/move")]
        public async Task Move(Guid id, MoveTaskCommand command, CancellationToken token)
        {
            await taskRepository.MoveTask(id, command.NewStageId, token);
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id, CancellationToken token)
        {
            await taskRepository.DeleteTask(id, token);
        }
    }
}