﻿using Castle.Core.Logging;
using FluentAssertions;
using KanbanBoard.Server.Controllers;
using KanbanBoard.Server.Repositories;
using KanbanBoard.Shared.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
            var invalidCommand = new PatchOrCreateTaskCommand() { Title = string.Empty };

            // Act  
            Func<Task> act =  async () => await sut.PatchOrCreate(Guid.NewGuid(), invalidCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
            repoMock.Verify(x => 
                x.PatchOrCreateTask(
                    It.IsAny<Guid>(),
                    It.IsAny<PatchOrCreateTaskCommand>(),
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
            var validCommand = new PatchOrCreateTaskCommand() { Title = "Valid" };
            var taskId = Guid.NewGuid();

            // Act  
            await sut.PatchOrCreate(taskId, validCommand, CancellationToken.None);

            // Assert
            repoMock.Verify(x =>
                x.PatchOrCreateTask(
                    taskId,
                    validCommand,
                    CancellationToken.None),
                Times.Once);
        }
    }
}
