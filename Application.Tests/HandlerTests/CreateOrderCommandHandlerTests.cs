using Application.Commands;
using AutoBogus;
using AutoMapper;
using Domain.Args;
using Domain.Services;
using Moq;

namespace Application.Tests.HandlerTests
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly Mock<IMapper> _mapper;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _orderService = new Mock<IOrderService>();
            _mapper = new Mock<IMapper>();
            _handler = new CreateOrderCommandHandler(_orderService.Object, _mapper.Object);
        }

        [Fact]
        public async Task Handle_ReturnsGuid()
        {
            // Arrange
            var orderId = AutoFaker.Generate<Guid>();
            var createOrderCommand = AutoFaker.Generate<CreateOrderCommand>();
            var args = AutoFaker.Generate<FillAddressArgs>();

            _mapper.Setup(m => m.Map<FillAddressArgs>(createOrderCommand))
                .Returns(args);

            _orderService.Setup(service => service.FillAddressAsync(args))
                .ReturnsAsync(orderId);

            // Act
            var result = await _handler.Handle(createOrderCommand, CancellationToken.None);

            // Assert
            Assert.Equal(orderId, result);
        }

        [Fact]
        public async Task Handle_ThrowsException()
        {
            // Arrange
            var createOrderCommand = AutoFaker.Generate<CreateOrderCommand>();

            _orderService.Setup(service => service.FillAddressAsync(It.IsAny<FillAddressArgs>()))
                .ThrowsAsync(new Exception("Service failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(createOrderCommand, CancellationToken.None));
        }
    }
}
