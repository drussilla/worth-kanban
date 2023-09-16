using KanbanBoard.Shared;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    public record FetchBoardsAction { }

    public record FetchBoardsResultAction(IEnumerable<BoardDto> Boards);

    public record SelectBoardAction(Guid Id);

    public record EditBoardAction(Guid Id);

    public record DeleteBoardAction(Guid Id, Guid? BoardToSelectNext);

    public record CancelEditBoardAction(Guid Id);

    public record SaveBoardAction(Guid Id, string Name);

    public record AddBoardAction(string Name);

    public record AddBoardResultAction(BoardDto Board);

}
