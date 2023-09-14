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

        [ReducerMethod]
        public static BoardsState ReduceAddTaskAction(BoardsState state, AddTaskAction action) 
        {
            var id = Guid.NewGuid();
            state.Boards[state.SelectedBoard!.Value].Stages[action.stageId].Tasks.Add(id, new TaskState(id, string.Empty, string.Empty, true));
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }
    }
}
