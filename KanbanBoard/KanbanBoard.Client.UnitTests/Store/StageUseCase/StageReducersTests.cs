using FluentAssertions;
using KanbanBoard.Client.Store.BoardUseCase;
using KanbanBoard.Client.Store.StageUseCase;


namespace KanbanBoard.Client.UnitTests.Store.StageUseCase
{
    public class StageReducersTests
    {
        [Fact]
        public void ReduceDeleteStageAction_ShouldDeleteExisting()
        {
            // Arrange
            var stage = new StageState(Guid.NewGuid(), "Stage1", new Dictionary<Guid, TaskState> { });
            var board = new BoardState(Guid.NewGuid(), "Board1", new Dictionary<Guid, StageState> { { stage.Id, stage } });
            var initialState = new BoardsState(false, new Dictionary<Guid, BoardState> { { board.Id, board } }, board.Id);

            var action = new DeleteStageAction(stage.Id);

            // Act
            var result = StageReducers.ReduceDeleteStageAction(initialState, action);

            // Assert
            result.Boards[board.Id].Stages.Should().BeEmpty();
        }

        [Fact]
        public void ReduceCancelEditStageAction_ShouldRemoveNonPersistedStage()
        {
            // Arrange
            var stage = new StageState(Guid.NewGuid(), "Stage1", new Dictionary<Guid, TaskState> { }, isPersisted: false, isEditing: true);
            var board = new BoardState(Guid.NewGuid(), "Board1", new Dictionary<Guid, StageState> { { stage.Id, stage } });
            var initialState = new BoardsState(false, new Dictionary<Guid, BoardState> { { board.Id, board } }, board.Id);

            var action = new CancelEditStageAction(stage.Id);

            // Act
            var result = StageReducers.ReduceCancelEditStageAction(initialState, action);

            // Assert
            result.Boards[board.Id].Stages.Should().BeEmpty();
        }

        [Fact]
        public void ReduceCancelEditStageAction_ShouldCancelEditForPersistedStage()
        {
            // Arrange
            var stage = new StageState(Guid.NewGuid(), "Stage1", new Dictionary<Guid, TaskState> { }, isPersisted: true, isEditing: true);
            var board = new BoardState(Guid.NewGuid(), "Board1", new Dictionary<Guid, StageState> { { stage.Id, stage } });
            var initialState = new BoardsState(false, new Dictionary<Guid, BoardState> { { board.Id, board } }, board.Id);

            var action = new CancelEditStageAction(stage.Id);

            // Act
            var result = StageReducers.ReduceCancelEditStageAction(initialState, action);

            // Assert
            result.Boards[board.Id].Stages.Should().NotBeEmpty();
            result.Boards[board.Id].Stages[stage.Id].IsEditing.Should().BeFalse();
        }
    }
}
