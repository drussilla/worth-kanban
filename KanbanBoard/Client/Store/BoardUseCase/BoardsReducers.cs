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
            state.Boards[state.SelectedBoard!.Value].Stages[action.stageId].Tasks.Add(id, new TaskState(id, id.ToString(), string.Empty, true));
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }

        [ReducerMethod]
        public static BoardsState ReduceMoveTaskAction(BoardsState state, MoveTaskAction action)
        {
            TaskState? task = null;
            foreach (var stage in state.GetSelectedBoard!.Stages.Values)
            {
                if (stage.Tasks.ContainsKey(action.taskId))
                {
                    task = stage.Tasks[action.taskId];
                    stage.Tasks.Remove(action.taskId);
                }
            }

            if (task == null)
            {
                throw new InvalidOperationException("Cannot move tasks that is not part of any Stage");
            }

            state.Boards[state.SelectedBoard!.Value].Stages[action.newStageId].Tasks.Add(task.Id, task);
            
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }
    }
}
