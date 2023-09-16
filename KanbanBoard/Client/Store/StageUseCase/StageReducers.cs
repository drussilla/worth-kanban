using Fluxor;
using KanbanBoard.Client.Store.BoardUseCase;
using KanbanBoard.Client.Store.TaskUseCase;

namespace KanbanBoard.Client.Store.StageUseCase
{
    public static class StageReducers
    {
        [ReducerMethod]
        public static BoardsState ReduceAddStageAction(BoardsState state, AddStageAction action)
        {
            var id = Guid.NewGuid();
            state.Boards[action.BoardId].Stages.Add(id, new StageState(id, string.Empty, new Dictionary<Guid, TaskState> (), isPersisted: false, isEditing: true));
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, state.IsEditing);
        }

        [ReducerMethod]
        public static BoardsState ReduceSaveStageAction(BoardsState state, SaveStageAction action)
        {
            var stageState = state.Boards[state.SelectedBoard!.Value].Stages[action.Id];
            state.Boards[state.SelectedBoard!.Value].Stages[action.Id] = new StageState(stageState.Id, action.Name, stageState.Tasks, isPersisted: stageState.IsPersisted, isEditing: false);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, state.IsEditing);
        }

        [ReducerMethod]
        public static BoardsState ReduceSavedStageAction(BoardsState state, SavedStageAction action)
        {
            var stageState = state.Boards[state.SelectedBoard!.Value].Stages[action.Id];
            state.Boards[state.SelectedBoard!.Value].Stages[action.Id] = new StageState(stageState.Id, stageState.Name, stageState.Tasks, isPersisted: true, isEditing: false);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, state.IsEditing);
        }

        [ReducerMethod]
        public static BoardsState ReduceEditStageAction(BoardsState state, EditStageAction action)
        {
            var stageState = state.Boards[state.SelectedBoard!.Value].Stages[action.Id];
            state.Boards[state.SelectedBoard!.Value].Stages[action.Id] = new StageState(stageState.Id, stageState.Name, stageState.Tasks, isPersisted: stageState.IsPersisted, isEditing: true);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, state.IsEditing);
        }

        [ReducerMethod]
        public static BoardsState ReduceCancelEditStageAction(BoardsState state, CancelEditStageAction action)
        {
            var stageState = state.Boards[state.SelectedBoard!.Value].Stages[action.Id];
            // If it is already saved in DB, just cancel editing
            if (stageState.IsPersisted)
            {
                state.Boards[state.SelectedBoard!.Value].Stages[action.Id] = new StageState(stageState.Id, stageState.Name, stageState.Tasks, stageState.IsPersisted, false);
            }
            // Otherwise remove it from the state
            else
            {
                state.Boards[state.SelectedBoard!.Value].Stages.Remove(action.Id);
            }

            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, state.IsEditing);
        }

        [ReducerMethod]
        public static BoardsState ReduceDeleteStageAction(BoardsState state, DeleteStageAction action)
        {
            if (!state.Boards[state.SelectedBoard!.Value].Stages.ContainsKey(action.Id))
            {
                throw new InvalidOperationException("Stage doesn't exists in the board");
            }

            if (state.Boards[state.SelectedBoard!.Value].Stages[action.Id].Tasks.Count > 0)
            {
                throw new InvalidOperationException("Cannot delete Stage with tasks");
            }

            state.Boards[state.SelectedBoard!.Value].Stages.Remove(action.Id);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard, state.IsEditing);
        }
    }
}
