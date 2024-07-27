using Application.Commands;
using AutoBogus;
using Domain.Models;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerService> _customerService;
        private readonly CreateCustomerCommandHandler _handler;

        public CreateCustomerCommandHandlerTests()
        {
            _customerService = new Mock<ICustomerService>();
            _handler = new CreateCustomerCommandHandler(_customerService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsGuid()
        {
            // Arrange
            var customerId = AutoFaker.Generate<Guid>();
            var createCustomerCommand = AutoFaker.Generate<CreateCustomerCommand>();
            var customer = AutoFaker.Generate<Customer>();

            _customerService.Setup(service => service.CreateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customerId);

            // Act
            var result = await _handler.Handle(createCustomerCommand, CancellationToken.None);

            // Assert
            Assert.Equal(customerId, result);
        }

        [Fact]
        public async Task Handle_ThrowsException()
        {
            // Arrange
            var createCustomerCommand = AutoFaker.Generate<CreateCustomerCommand>();

            _customerService.Setup(service => service.CreateAsync(It.IsAny<Customer>()))
                .ThrowsAsync(new Exception("Service failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(createCustomerCommand, CancellationToken.None));
        }
    }
}
