using KanbanBoard.Server.Data;
using KanbanBoard.Server.Repositories;
using KanbanBoard.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace KanbanBoard.Server.UnitTests.Repositories
{
    public class StageRepositoryTest
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
            
            var orderGeneratorMock = new Mock<StageOrderGenerator>();
            orderGeneratorMock
                .Setup(x => x.GenerateIntialOrder())
                .Returns(1);

            var id = Guid.NewGuid();
            
            var title = "test";

            var sut = new StageRepository(dummyContext, orderGeneratorMock.Object, new Mock<ILogger<StageRepository>>().Object);

            // Act
            await sut.CreateStageAsync(id, boardId, title, CancellationToken.None);

            // Assert
            orderGeneratorMock.VerifyAll();
        }

        private ApplicationDbContext CreateContext()
        {
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;
        }
    }
}
