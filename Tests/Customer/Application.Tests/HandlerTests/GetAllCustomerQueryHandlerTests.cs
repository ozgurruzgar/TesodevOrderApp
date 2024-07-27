using Application.Queries;
using Domain.Models;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class GetAllCustomerQueryHandlerTests
    {
        private readonly Mock<ICustomerService> _customerService;
        private readonly GetAllCustomerQueryHandler _handler;

        public GetAllCustomerQueryHandlerTests()
        {
            _customerService = new Mock<ICustomerService>();
            _handler = new GetAllCustomerQueryHandler(_customerService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsCustomerList()
        {
            // Arrange
            var expectedCustomers = new List<Customer>
        {
            new Customer { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" },
            new Customer { Id = Guid.NewGuid(), Name = "Jane Doe", Email = "jane.doe@example.com" }
        };

            _customerService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedCustomers);

            // Act
            var result = await _handler.Handle(new GetAllCustomerQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(expectedCustomers, result);
            _customerService.Verify(service => service.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList()
        {
            // Arrange
            var expectedCustomers = new List<Customer>();

            _customerService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedCustomers);

            // Act
            var result = await _handler.Handle(new GetAllCustomerQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(expectedCustomers, result);
            _customerService.Verify(service => service.GetAllAsync(), Times.Once);
        }
    }
}
