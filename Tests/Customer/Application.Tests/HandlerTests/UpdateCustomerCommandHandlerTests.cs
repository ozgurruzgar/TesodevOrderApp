using Application.Commands;
using AutoBogus;
using Domain.Models;
using Domain.Services;
using Moq;
using TesodevOrderApp.Shared.Domain.Models;

namespace Application.Tests.HandlerTests
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerService> _customerService;
        private readonly UpdateCustomerCommandHandler _handler;

        public UpdateCustomerCommandHandlerTests()
        {
            _customerService = new Mock<ICustomerService>();
            _handler = new UpdateCustomerCommandHandler(_customerService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTrue()
        {
            // Arrange
            var updateCustomerCommand = AutoFaker.Generate<UpdateCustomerCommand>();

            _customerService.Setup(service => service.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(updateCustomerCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ReturnsFalse()
        {
            // Arrange
            var updateCustomerCommand = AutoFaker.Generate<UpdateCustomerCommand>(); 

            _customerService.Setup(service => service.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(updateCustomerCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
