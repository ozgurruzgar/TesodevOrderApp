using Application.Commands;
using AutoBogus;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class DeleteCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerService> _customerService;
        private readonly DeleteCustomerCommandHandler _handler;

        public DeleteCustomerCommandHandlerTests()
        {
            _customerService = new Mock<ICustomerService>();
            _handler = new DeleteCustomerCommandHandler(_customerService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTrue()
        {
            // Arrange
            var customerId = AutoFaker.Generate<Guid>();

            _customerService.Setup(service => service.DeleteAsync(customerId))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(new DeleteCustomerCommand { Id = customerId }, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ReturnsFalse()
        {
            // Arrange
            var customerId = AutoFaker.Generate<Guid>();

            _customerService.Setup(service => service.DeleteAsync(customerId))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(new DeleteCustomerCommand { Id = customerId }, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
