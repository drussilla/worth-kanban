using Fluxor;
using KanbanBoard.Client.Store.BoardUseCase;

namespace KanbanBoard.Client.Store.StageUseCase
{
    public static class StageReducers
    {
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
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }
    }
}
