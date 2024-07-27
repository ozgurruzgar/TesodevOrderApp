using Application.Commands;
using AutoBogus;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class ChangeStatusCommandHandlerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly ChangeStatusCommandHandler _handler;

        public ChangeStatusCommandHandlerTests()
        {
            _orderService = new Mock<IOrderService>();
            _handler = new ChangeStatusCommandHandler(_orderService.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTrue()
        {
            // Arrange
            var command = AutoFaker.Generate<ChangeStatusCommand>();

            _orderService.Setup(service => service.ChangeStatusAsync(command.Id, command.Status))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_ReturnsFalse()
        {
            // Arrange
            var changeStatusCommand = AutoFaker.Generate<ChangeStatusCommand>();

            _orderService.Setup(service => service.ChangeStatusAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Service failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(changeStatusCommand, CancellationToken.None));
        }
    }
}
