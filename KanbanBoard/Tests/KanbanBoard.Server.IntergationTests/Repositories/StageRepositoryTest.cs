using FluentAssertions;
using KanbanBoard.Server.Data;
using KanbanBoard.Server.IntergationTests;
using KanbanBoard.Server.Repositories;
using KanbanBoard.Server.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace KanbanBoard.Server.UnitTests.Repositories
{
    public class StageRepositoryTest : DbContextTest
    {
        [Fact]
        public async Task CreateStageAsync_ShouldCreateStageWithTheRightOrder_WhenNoOtherStagesExists()
        {
            // Arrange
            var boardId = Guid.NewGuid();

            using var dummyContext = CreateContext();
            dummyContext.Boards.Add(new Models.Board()
            {
                Id = boardId,
                Name = "Test",
            });
            await dummyContext.SaveChangesAsync();

            var orderGeneratorMock = new Mock<IStageOrderGenerator>();
            orderGeneratorMock
                .Setup(x => x.GenerateIntialOrder())
                .Returns(1);

            var id = Guid.NewGuid();

            var title = "test";

            var sut = new StageRepository(dummyContext, orderGeneratorMock.Object, new Mock<ILogger<StageRepository>>().Object);

            // Act
            await sut.CreateOrUpdateStageAsync(id, boardId, title, CancellationToken.None);
            var result = dummyContext.Stages.First(x => x.Id == id);

            // Assert
            orderGeneratorMock.VerifyAll();
            result.Order.Should().Be(1);
            result.Name.Should().Be(title);
            result.BoardId.Should().Be(boardId);
        }

        [Fact]
        public async Task CreateStageAsync_ShouldCreateStageWithTheRightOrder_WhenOtherStagesExist()
        {
            // Arrange
            var boardId = Guid.NewGuid();

            using var dummyContext = CreateContext();
            dummyContext.Boards.Add(new Models.Board()
            {
                Id = boardId,
                Name = "Test",
            });
            dummyContext.Stages.Add(new Models.Stage()
            {
                Id = Guid.NewGuid(),
                BoardId = boardId,
                Name = "test2",
                Order = 1
            });

            await dummyContext.SaveChangesAsync();

            var orderGeneratorMock = new Mock<IStageOrderGenerator>();
            orderGeneratorMock
                .Setup(x => x.GenerateOrder(1, null))
                .Returns(2);

            var id = Guid.NewGuid();

            var title = "test";

            var sut = new StageRepository(dummyContext, orderGeneratorMock.Object, new Mock<ILogger<StageRepository>>().Object);

            // Act
            await sut.CreateOrUpdateStageAsync(id, boardId, title, CancellationToken.None);
            var result = dummyContext.Stages.First(x => x.Id == id);

            // Assert
            orderGeneratorMock.VerifyAll();
            result.Order.Should().Be(2);
            result.Name.Should().Be(title);
            result.BoardId.Should().Be(boardId);
        }
    }
}
