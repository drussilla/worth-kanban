using KanbanBoard.Server.Repositories;
using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StageController : ControllerBase
    {
        private readonly IStageRepository stageRepository;
        private readonly ILogger<StageController> logger;

        public StageController(IStageRepository stageRepository, ILogger<StageController> logger)
        {
            this.stageRepository = stageRepository;
            this.logger = logger;
        }

        [HttpPost]
        public async Task Create(CreateStageCommand command, CancellationToken token) 
        {
            if (!command.IsValid())
            {
                logger.LogError($"Invalid command to create stage!");
                throw new ArgumentException("Command object is not valid");
            }

            await stageRepository.CreateStageAsync(command.Id, command.BoardId, command.Title, token);
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id, CancellationToken token)
        {
            await stageRepository.DeleteStage(id, token);
        }
    }
}
