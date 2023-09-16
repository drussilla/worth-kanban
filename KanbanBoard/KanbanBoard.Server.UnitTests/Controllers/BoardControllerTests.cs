using FluentAssertions;
using KanbanBoard.Server.Controllers;
using KanbanBoard.Server.Repositories.Interfaces;
using KanbanBoard.Shared.Requests;
using Microsoft.Extensions.Logging;
using Moq;

namespace KanbanBoard.Server.UnitTests.Controllers
{
    public class BoardControllerTests
    {
        [Fact]
        public async Task Create_ShouldCreateBoardAndReturnDtoWithStages()
        {
            // Arrange
            var name = "Name";
            var board = new Models.Board { Name = name };
            board.Stages.Add(new Models.Stage { Name = "Test" });

            var repoMock = new Mock<IBoardRepository>();
            repoMock
                .Setup(x => x.CreateAsync(name, CancellationToken.None))
                .ReturnsAsync(board);

            var logger = new Mock<ILogger<BoardController>>();
            var request = new CreateBoardRequest() { Name = name };

            var sut = new BoardController(repoMock.Object, logger.Object);

            // Act  
            var result = await sut.Create(request, CancellationToken.None);

            // Assert
            repoMock.VerifyAll();
            result.Name.Should().Be(name);
            result.Stages.Should().HaveCount(1);
            result.Stages.First().Name.Should().Be("Test");
        }
    }
}
