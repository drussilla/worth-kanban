using Fluxor;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    public static class BoardsReducers
    {
        [ReducerMethod]
        public static BoardsState ReduceFetchBoardsAction(BoardsState state, FetchBoardsAction action) => 
            new(isLoading: true, boards: new Dictionary<Guid, BoardState>(), null);

        [ReducerMethod]
        public static BoardsState ReduceFetchBoardsResuleAction(BoardsState state, FetchBoardsResultAction action) => 
            new (isLoading: false, boards: action.Boards.ToDictionary(key => key.Id, value => new BoardState(value)), null);

        [ReducerMethod]
        public static BoardsState ReduceSelectBoardAction(BoardsState state, SelectBoardAction action) =>
            new(isLoading: state.IsLoading, boards: state.Boards, action.Id);
        
    }
}
