using KanbanBoard.Shared;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    public record FetchBoardsAction { }

    public record FetchBoardsResultAction(IEnumerable<BoardDto> Boards);

    public record SelectBoardAction(Guid Id);
    
    public record AddTaskAction(Guid stageId);

    public record MoveTaskAction(Guid taskId, Guid newStageId);
}
