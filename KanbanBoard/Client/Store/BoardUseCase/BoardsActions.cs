using KanbanBoard.Shared;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    public record FetchBoardsAction { }

    public record FetchBoardsResultAction(IEnumerable<BoardDto> Boards);

    public record SelectBoardAction(Guid Id);  

}
