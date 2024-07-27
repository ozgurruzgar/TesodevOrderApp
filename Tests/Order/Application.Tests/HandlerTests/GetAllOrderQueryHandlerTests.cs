using Application.Queries;
using AutoBogus;
using Domain.Models;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class GetAllOrderQueryHandlerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly GetAllOrderQueryHandler _handler;

        public GetAllOrderQueryHandlerTests()
        {
            _orderService = new Mock<IOrderService>();
            _handler = new GetAllOrderQueryHandler(_orderService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsOrderList()
        {
            // Arrange
            var expectedOrders = AutoFaker.Generate<List<Order>>();

            _orderService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedOrders);

            // Act
            var result = await _handler.Handle(new GetAllOrderQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(expectedOrders, result);
            _orderService.Verify(service => service.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList()
        {
            // Arrange
            var expectedOrders = new List<Order>();

            _orderService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedOrders);

            // Act
            var result = await _handler.Handle(new GetAllOrderQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(expectedOrders, result);
            _orderService.Verify(service => service.GetAllAsync(), Times.Once);
        }
    }
}
