using FluentAssertions;
using KanbanBoard.Server.Models;
using KanbanBoard.Server.Repositories;
using KanbanBoard.Server.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace KanbanBoard.Server.IntergationTests.Repositories
{
    public class BoardRepositoryTests : DbContextTest
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateBoardAsync_ShouldCreateBoardWithDefaultStages()
        {
            // Arrange
            var boardId = Guid.NewGuid();

            using var dummyContext = CreateContext();
            
            var providerMock = new Mock<IDefaultStagesProvider>();
            providerMock
                .Setup(x => x.GetDefaultStages())
                .Returns(new List<Stage>{ new Stage { Id = Guid.NewGuid(), Name = "Test1"}, new Stage { Id = Guid.NewGuid(), Name = "Test2" } });

            var id = Guid.NewGuid();

            var title = "test";

            var sut = new BoardRepository(dummyContext, providerMock.Object, new Mock<ILogger<BoardRepository>>().Object);

            // Act
            var result = await sut.CreateAsync(title, CancellationToken.None);
            
            // Assert
            providerMock.VerifyAll();
            result.Stages.Should().HaveCount(2);
            result.Name.Should().Be(title);
        }
    }
}
