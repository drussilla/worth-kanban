using KanbanBoard.Server.Data;
using KanbanBoard.Server.Models;
using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Server.Repositories
{
    public class StageRepository : IStageRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IStageOrderGenerator orderGenerator;
        private readonly ILogger<StageRepository> logger;

        public StageRepository(ApplicationDbContext context, IStageOrderGenerator orderGenerator, ILogger<StageRepository> logger)
        {
            this.context = context;
            this.orderGenerator = orderGenerator;
            this.logger = logger;
        }

        public async System.Threading.Tasks.Task CreateStageAsync(Guid id, Guid boardId, string title, CancellationToken token)
        {
            // Get the highest order to create new stage at the end
            var maxStageOrderInBoard = await context
                .Stages
                .Where(x => x.BoardId == boardId)
                .Select(x => x.Order)
                .DefaultIfEmpty()
                .MaxAsync(token);

            uint order = maxStageOrderInBoard == 0 
                ? orderGenerator.GenerateIntialOrder()
                : orderGenerator.GenerateOrder(maxStageOrderInBoard);

            logger.LogInformation($"Generated order for the new stage: {order}");

            var stage = new Stage
            {
                Id = id,
                BoardId = boardId,
                Name = title,
                Order = order
            };

            context.Stages.Add(stage);
            await context.SaveChangesAsync(token);
        }

        public async System.Threading.Tasks.Task DeleteStage(Guid id, CancellationToken token)
        {
            // I relly on DB contexts to thorw an error if task is not in the repo. This logic can be moved one lever higher for a proper handling.
            var stage = await context.Stages.FirstAsync(t => t.Id == id, token);
            context.Stages.Remove(stage);
            await context.SaveChangesAsync(token);
        }

        public System.Threading.Tasks.Task UpdateTitleAsync(Guid id, string title, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
