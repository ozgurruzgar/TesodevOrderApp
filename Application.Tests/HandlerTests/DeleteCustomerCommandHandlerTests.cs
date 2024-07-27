using Application.Commands;
using AutoBogus;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class DeleteOrderCommandHandlerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly DeleteOrderCommandHandler _handler;

        public DeleteOrderCommandHandlerTests()
        {
            _orderService = new Mock<IOrderService>();
            _handler = new DeleteOrderCommandHandler(_orderService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTrue()
        {
            // Arrange
            var orderId = AutoFaker.Generate<Guid>();

            _orderService.Setup(service => service.DeleteAsync(orderId))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(new DeleteOrderCommand { Id = orderId }, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ReturnsFalse()
        {
            // Arrange
            var orderId = AutoFaker.Generate<Guid>();

            _orderService.Setup(service => service.DeleteAsync(orderId))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(new DeleteOrderCommand { Id = orderId }, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
