using KanbanBoard.Client.Services;
using KanbanBoard.Client.Store.TaskUseCase;
using Microsoft.Extensions.Logging;
using Moq;

namespace KanbanBoard.Client.UnitTests.Store.TaskUseCase
{
    public class TaskEffectsTests
    {
        [Fact]
        public async Task HandleMoveTaskAction_ShouldInvokeBoardsService()
        {
            // Arrange
            var serviceMock = new Mock<IBoardService>();
            var loggerMock = new Mock<ILogger<TaskEffects>>();

            var taskId = Guid.NewGuid();
            var newStageId = Guid.NewGuid();

            var sut = new TaskEffects(serviceMock.Object, loggerMock.Object);

            // Act
            await sut.HandleMoveTaskAction(new MoveTaskAction(taskId, newStageId), null);

            // Assert
            serviceMock.Verify(
                x => x.MoveTaskToStageAsync(taskId, newStageId),
                Times.Once);
        }

        [Fact]
        public async Task HandleDeleteTaskAction_ShouldInvokeBoardsService()
        {
            // Arrange
            var serviceMock = new Mock<IBoardService>();
            var loggerMock = new Mock<ILogger<TaskEffects>>();

            var taskId = Guid.NewGuid();
            var stageId = Guid.NewGuid();

            var sut = new TaskEffects(serviceMock.Object, loggerMock.Object);

            // Act
            await sut.HandleDeleteTaskAction(new DeleteTaskAction(taskId, stageId), null);

            // Assert
            serviceMock.Verify(
                x => x.DeleteTaskAsync(taskId),
                Times.Once);
        }

        [Fact]
        public async Task HandleSaveTaskAction_ShouldInvokeBoardsService()
        {
            // Arrange
            var serviceMock = new Mock<IBoardService>();
            var loggerMock = new Mock<ILogger<TaskEffects>>();

            var taskId = Guid.NewGuid();
            var stageId = Guid.NewGuid();
            var boardId = Guid.NewGuid();
            var title = "Test";
            var description = "Desc";

            var sut = new TaskEffects(serviceMock.Object, loggerMock.Object);

            // Act
            await sut.HandleSaveTaskAction(new SaveTaskAction(taskId, stageId, boardId, title, description), null);

            // Assert
            serviceMock.Verify(
                x => x.UpdateOrCreateTaskAsync(taskId, stageId, boardId, title, description),
                Times.Once);
        }
    }
}
