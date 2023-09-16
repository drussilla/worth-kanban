using FluentAssertions;
using KanbanBoard.Server.Services;

namespace KanbanBoard.Server.UnitTests.Services
{
    public class StageOrderGeneratorTests
    {
        [Fact]
        public void GenerateOrder_ShouldReturnFullStepOrder_WhenNextOrderIsNotProvider()
        {
            // Arrange
            var sut = new StageOrderGenerator();

            // Act
            var result = sut.GenerateOrder(0);

            // Assert
            result.Should().Be(100_000);
        }

        [Fact]
        public void GenerateOrder_ShouldThrow_WhenThereIsNoGapBetweenPreviousAndNextOrder()
        {
            // Arrange
            var sut = new StageOrderGenerator();

            // Act
            var act = () => sut.GenerateOrder(0, 1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void GenerateOrder_ShouldReturnOrderInBetweenPreviousAndNext_WhenBothProvided()
        {
            // Arrange
            var sut = new StageOrderGenerator();

            // Act
            var result = sut.GenerateOrder(0, 2);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public void GenerateIntialOrder_ShouldReturnInitialOrder()
        {
            // Arrange
            var sut = new StageOrderGenerator();

            // Act
            var result = sut.GenerateIntialOrder();

            // Assert
            result.Should().Be(100_000);
        }
    }
}
