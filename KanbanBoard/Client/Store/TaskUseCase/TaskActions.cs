namespace KanbanBoard.Client.Store.TaskUseCase
{
    public record AddTaskAction(Guid StageId);

    public record MoveTaskAction(Guid TaskId, Guid NewStageId);

    public record EditTaskAction(Guid TaskId, Guid StageId);

    public record SaveTaskAction(Guid TaskId, Guid StageId, Guid BoardId, string Title, string Description);
}
