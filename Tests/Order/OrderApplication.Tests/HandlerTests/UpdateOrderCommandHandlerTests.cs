using Application.Commands;
using AutoBogus;
using Domain.Args;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly UpdateOrderCommandHandler _handler;

        public UpdateOrderCommandHandlerTests()
        {
            _orderService = new Mock<IOrderService>();
            _handler = new UpdateOrderCommandHandler(_orderService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTrue()
        {
            // Arrange
            var updateOrderCommand = AutoFaker.Generate<UpdateOrderCommand>();

            _orderService.Setup(service => service.UpdateAsync(It.IsAny<UpdateOrderArgs>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(updateOrderCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ReturnsFalse()
        {
            // Arrange
            var updateOrderCommand = AutoFaker.Generate<UpdateOrderCommand>();

            _orderService.Setup(service => service.UpdateAsync(It.IsAny<UpdateOrderArgs>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(updateOrderCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
