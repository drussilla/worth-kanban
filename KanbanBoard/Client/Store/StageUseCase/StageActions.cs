namespace KanbanBoard.Client.Store.StageUseCase
{
    public record AddStageAction(Guid BoardId);

    public record SaveStageAction(Guid Id, Guid BoardId, string Name);

    public record SavedStageAction(Guid Id);

    public record EditStageAction(Guid Id);

    public record DeleteStageAction(Guid Id);

    public record CancelEditStageAction(Guid Id);
}
