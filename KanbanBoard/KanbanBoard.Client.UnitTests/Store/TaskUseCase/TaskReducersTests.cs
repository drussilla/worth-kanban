using FluentAssertions;
using KanbanBoard.Client.Store.BoardUseCase;
using KanbanBoard.Client.Store.TaskUseCase;

namespace KanbanBoard.Client.UnitTests.Store.TaskUseCase
{
    public class TaskReducersTests
    {
        [Fact]
        public void ReduceMoveTaskAction_ShouldMoveTaskToNewStage()
        {
            // Arrange
            var task = new TaskState(Guid.NewGuid(), "Task1", "Descr", true, false);
            var stage1 = new StageState(Guid.NewGuid(), "Stage1", new Dictionary<Guid, TaskState> { { task.Id, task } });
            var stage2 = new StageState(Guid.NewGuid(), "Stage2", new Dictionary<Guid, TaskState>());
            var board = new BoardState(Guid.NewGuid(), "Board1", new Dictionary<Guid, StageState> { { stage1.Id, stage1 }, { stage2.Id, stage2 } });
            var initialState = new BoardsState(false, new Dictionary<Guid, BoardState> { { board.Id, board } }, board.Id, false);

            var action = new MoveTaskAction(task.Id, stage2.Id);

            // Act
            var result = TaskReducers.ReduceMoveTaskAction(initialState, action);

            // Assert
            result.Boards[board.Id].Stages[stage1.Id].Tasks.Should().BeEmpty();
            result.Boards[board.Id].Stages[stage2.Id].Tasks.Should().ContainKey(task.Id);
        }

        [Fact]
        public void ReduceMoveTaskAction_ShouldThrow_WhenBoardIsNotSelected()
        {
            // Arrange
            var initialState = new BoardsState(false, new Dictionary<Guid, BoardState>(), null, false);

            var action = new MoveTaskAction(Guid.NewGuid(), Guid.NewGuid());

            // Act
            var act = () => TaskReducers.ReduceMoveTaskAction(initialState, action);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
