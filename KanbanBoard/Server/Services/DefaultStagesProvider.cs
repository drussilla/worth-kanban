using KanbanBoard.Server.Models;

namespace KanbanBoard.Server.Services
{
    public class DefaultStagesProvider : IDefaultStagesProvider
    {
        private readonly IStageOrderGenerator orderGenerator;

        public DefaultStagesProvider(IStageOrderGenerator orderGenerator)
        {
            this.orderGenerator = orderGenerator;
        }

        public List<Stage> GetDefaultStages()
        {
            List<Stage> stages = new List<Stage>();
            var order = orderGenerator.GenerateIntialOrder();
            stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Backlogs",
                Order = order,
            });

            order = orderGenerator.GenerateOrder(order);
            stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Planned",
                Order = order
            });

            order = orderGenerator.GenerateOrder(order);
            stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "In Progress",
                Order = order
            });

            order = orderGenerator.GenerateOrder(order);
            stages.Add(new Stage()
            {
                Id = Guid.NewGuid(),
                Name = "Completed",
                Order = order
            });

            return stages;
        }
    }
}
