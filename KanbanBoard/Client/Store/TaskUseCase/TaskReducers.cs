using Fluxor;
using KanbanBoard.Client.Store.BoardUseCase;

namespace KanbanBoard.Client.Store.TaskUseCase
{
    public static class TaskReducers
    {
        [ReducerMethod]
        public static BoardsState ReduceAddTaskAction(BoardsState state, AddTaskAction action)
        {
            var id = Guid.NewGuid();
            state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks.Add(id, new TaskState(id, string.Empty, string.Empty, true));
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }

        [ReducerMethod]
        public static BoardsState ReduceMoveTaskAction(BoardsState state, MoveTaskAction action)
        {
            // Find task in old Stage
            TaskState? task = null;
            if (state.SelectedBoard == null) 
            {
                throw new InvalidOperationException("Cannot move task is board is not selected");
            }

            foreach (var stage in state.GetSelectedBoard!.Stages.Values)
            {
                if (stage.Tasks.ContainsKey(action.TaskId))
                {
                    task = stage.Tasks[action.TaskId];
                    stage.Tasks.Remove(action.TaskId);
                }
            }

            if (task == null)
            {
                throw new InvalidOperationException("Cannot move tasks that is not part of any Stage");
            }

            // Move to new stage
            state.Boards[state.SelectedBoard!.Value].Stages[action.NewStageId].Tasks.Add(task.Id, task);

            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }

        [ReducerMethod]
        public static BoardsState ReduceEditTaskAction(BoardsState state, EditTaskAction action)
        {
            var taskState = state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks[action.TaskId];
            state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks[action.TaskId] = new TaskState(taskState.Id, taskState.Title, taskState.Description, true);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }

        [ReducerMethod]
        public static BoardsState ReduceCancelTaskEditAction(BoardsState state, CancelTaskEditAction action)
        {
            var taskState = state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks[action.TaskId];
            state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks[action.TaskId] = new TaskState(taskState.Id, taskState.Title, taskState.Description, false);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }

        [ReducerMethod]
        public static BoardsState ReduceSaveTaskAction(BoardsState state, SaveTaskAction action)
        {
            var taskState = state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks[action.TaskId];
            state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks[action.TaskId] = new TaskState(taskState.Id, action.Title, action.Description, false);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }

        [ReducerMethod]
        public static BoardsState ReduceDeleteTaskAction(BoardsState state, DeleteTaskAction action)
        {
            state.Boards[state.SelectedBoard!.Value].Stages[action.StageId].Tasks.Remove(action.TaskId);
            return new(isLoading: state.IsLoading, boards: state.Boards, state.SelectedBoard);
        }
    }
}
