using Application.Queries;
using AutoBogus;
using Domain.Models;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly GetOrderByIdQueryHandler _handler;

        public GetOrderByIdQueryHandlerTests()
        {
            _orderService = new Mock<IOrderService>();
            _handler = new GetOrderByIdQueryHandler(_orderService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsOrder()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedOrder= AutoFaker.Generate<Order>();

            _orderService.Setup(service => service.GetOrderByIdAsync(orderId))
                .ReturnsAsync(expectedOrder);

            // Act
            var result = await _handler.Handle(new GetOrderByIdQuery { Id = orderId }, CancellationToken.None);

            // Assert
            Assert.Equal(expectedOrder, result);
            _orderService.Verify(service => service.GetOrderByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsNull()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _orderService.Setup(service => service.GetOrderByIdAsync(orderId))
                .ReturnsAsync(It.IsAny<Order>());

            // Act
            var result = await _handler.Handle(new GetOrderByIdQuery { Id = orderId }, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _orderService.Verify(service => service.GetOrderByIdAsync(orderId), Times.Once);
        }
    }
}
