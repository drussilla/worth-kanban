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
    }
}
