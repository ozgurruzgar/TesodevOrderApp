using Application.Queries;
using AutoBogus;
using Domain.Models;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class GetOrderByCustomerIdQueryHandlerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly GetOrderByCustomerIdQueryHandler _handler;

        public GetOrderByCustomerIdQueryHandlerTests()
        {
            _orderService = new Mock<IOrderService>();
            _handler = new GetOrderByCustomerIdQueryHandler(_orderService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsOrderListsByCustomerId()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedOrder = AutoFaker.Generate<List<Order>>();

            _orderService.Setup(service => service.GetOrderByCustomerIdAsync(customerId))
                .ReturnsAsync(expectedOrder);

            // Act
            var result = await _handler.Handle(new GetOrderByCustomerIdQuery { CustomerId = customerId }, CancellationToken.None);

            // Assert
            Assert.Equal(expectedOrder, result);
            _orderService.Verify(service => service.GetOrderByCustomerIdAsync(customerId), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            _orderService.Setup(service => service.GetOrderByCustomerIdAsync(customerId))
                .ReturnsAsync(It.IsAny<List<Order>>());

            // Act
            var result = await _handler.Handle(new GetOrderByCustomerIdQuery { CustomerId = customerId }, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _orderService.Verify(service => service.GetOrderByCustomerIdAsync(customerId), Times.Once);
        }
    }
}
