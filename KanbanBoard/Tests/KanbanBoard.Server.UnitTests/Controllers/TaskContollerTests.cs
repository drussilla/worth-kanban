﻿using FluentAssertions;
using KanbanBoard.Server.Controllers;
using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Shared.Requests;
using Microsoft.Extensions.Logging;
using Moq;

namespace KanbanBoard.Server.UnitTests.Controllers
{
    public class TaskContollerTests
    {
        [Fact]
        public async Task Patch_ShouldThrowException_WhenCommandIsNotValid()
        {
            // Arrange
            var repoMock = new Mock<ITaskRepository>();
            var logger = new Mock<ILogger<TaskController>>();
            var sut = new TaskController(repoMock.Object, logger.Object);
            var invalidCommand = new UpdateOrCreateTaskRequest() { Title = string.Empty };

            // Act  
            Func<Task> act =  async () => await sut.UpdateOrCreate(Guid.NewGuid(), invalidCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
            repoMock.Verify(x => 
                x.UpdateOrCreateTask(
                    It.IsAny<Guid>(),
                    It.IsAny<UpdateOrCreateTaskRequest>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Patch_ShouldCallRepository_WhenCommandIsValid()
        {
            // Arrange
            var repoMock = new Mock<ITaskRepository>();
            var logger = new Mock<ILogger<TaskController>>();
            var sut = new TaskController(repoMock.Object, logger.Object);
            var validCommand = new UpdateOrCreateTaskRequest() { Title = "Valid" };
            var taskId = Guid.NewGuid();

            // Act  
            await sut.UpdateOrCreate(taskId, validCommand, CancellationToken.None);

            // Assert
            repoMock.Verify(x =>
                x.UpdateOrCreateTask(
                    taskId,
                    validCommand,
                    CancellationToken.None),
                Times.Once);
        }
    }
}
