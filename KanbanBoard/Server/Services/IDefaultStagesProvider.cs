namespace KanbanBoard.Server.Services
{
    public interface IDefaultStagesProvider
    {
        List<Models.Stage> GetDefaultStages();
    }
}
