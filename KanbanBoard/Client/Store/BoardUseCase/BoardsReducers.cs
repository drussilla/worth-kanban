using Fluxor;

namespace KanbanBoard.Client.Store.BoardUseCase
{
    public static class BoardsReducers
    {
        [ReducerMethod]
        public static BoardsState ReduceFetchBoardsAction(BoardsState state, FetchBoardsAction action) =>
            new(isLoading: true, boards: new Dictionary<Guid, BoardState>(), null, false);

        [ReducerMethod]
        public static BoardsState ReduceFetchBoardsResuleAction(BoardsState state, FetchBoardsResultAction action) =>
            new(isLoading: false, boards: action.Boards.ToDictionary(key => key.Id, value => new BoardState(value)), null, false);

        [ReducerMethod]
        public static BoardsState ReduceSelectBoardAction(BoardsState state, SelectBoardAction action) =>
            new(isLoading: state.IsLoading, boards: state.Boards, action.Id, state.IsEditing);

        [ReducerMethod]
        public static BoardsState ReduceEditBoardAction(BoardsState state, EditBoardAction action) =>
            new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, true);

        [ReducerMethod]
        public static BoardsState ReduceSaveBoardAction(BoardsState state, SaveBoardAction action)
        {
            var boardState = state.Boards[action.Id];
            state.Boards[action.Id] = new BoardState(boardState.Id, action.Name, boardState.Stages);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, false);
        }

        [ReducerMethod]
        public static BoardsState ReduceCanbceEditBoardAction(BoardsState state, CancelEditBoardAction action) =>
            new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, false);

        [ReducerMethod]
        public static BoardsState ReduceCAction(BoardsState state, AddBoardResultAction action)
        {
            state.Boards.Add(action.Board.Id, new BoardState(action.Board));
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, false);
        }
    }
}
