using Application.Queries;
using Domain.Models;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class GetCustomerByIdQueryHandlerTests
    {
        private readonly Mock<ICustomerService> _customerService;
        private readonly GetCustomerByIdQueryHandler _handler;

        public GetCustomerByIdQueryHandlerTests()
        {
            _customerService = new Mock<ICustomerService>();
            _handler = new GetCustomerByIdQueryHandler(_customerService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedCustomer = new Customer 
            { 
                Id = customerId,
                Name = "John Doe",
                Email = "john.doe@example.com" 
            };

            _customerService.Setup(service => service.GetByIdAsync(customerId))
                .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _handler.Handle(new GetCustomerByIdQuery { Id = customerId }, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCustomer, result);
            _customerService.Verify(service => service.GetByIdAsync(customerId), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            _customerService.Setup(service => service.GetByIdAsync(customerId))
                .ReturnsAsync(It.IsAny<Customer>());

            // Act
            var result = await _handler.Handle(new GetCustomerByIdQuery { Id = customerId }, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _customerService.Verify(service => service.GetByIdAsync(customerId), Times.Once);
        }
    }
}
