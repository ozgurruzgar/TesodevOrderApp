using Application.Commands;
using Application.Queries;
using AutoBogus;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models.Request;
using WebAPI.Models.Response;

namespace WebAPI.Tests.ControllerTests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IMapper> _mapper;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _mapper = new Mock<IMapper>();
            _controller = new OrdersController(_mediator.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllOrder_ReturnsOkResult()
        {
            // Arrange
            var query = AutoFaker.Generate<GetAllOrderQuery>();
            var expectedResponse = AutoFaker.Generate<List<Order>>();

            _mediator.Setup(m => m.Send(It.IsAny<GetAllOrderQuery>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAllOrder();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetOrderById_ReturnsOkResult()
        {
            // Arrange
            var query = AutoFaker.Generate<GetOrderByIdQuery>();
            var response = AutoFaker.Generate<Order>();
            var mappedResponse = new GetOrderByIdResponseModel();

            _mediator.Setup(m => m.Send(It.IsAny<GetOrderByIdQuery>(), default))
                .ReturnsAsync(response);

            _mapper.Setup(m => m.Map<GetOrderByIdResponseModel>(response))
                .Returns(mappedResponse);

            // Act
            var result = await _controller.GetOrderById(query.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetOrderById_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = AutoFaker.Generate<GetOrderByIdQuery>();
            query.Id = id;

            _mediator.Setup(m => m.Send(query, default))
                .ReturnsAsync(It.IsAny<Order>());

            // Act
            var result = await _controller.GetOrderById(id);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetOrderByCustomerId_ReturnsOkResult()
        {
            // Arrange
            var query = AutoFaker.Generate<GetOrderByCustomerIdQuery>();
            var response = AutoFaker.Generate<List<Order>>();
            var mappedResponse = new GetOrderByCustomerIdResponseModel();

            _mediator.Setup(m => m.Send(It.IsAny<GetOrderByCustomerIdQuery>(), default))
                .ReturnsAsync(response);

            _mapper.Setup(m => m.Map<GetOrderByCustomerIdResponseModel>(response))
                .Returns(mappedResponse);

            // Act
            var result = await _controller.GetOrderByCustomerId(query.CustomerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetOrderByCustomerId_ReturnsNotFoundResult()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var query = AutoFaker.Generate<GetOrderByCustomerIdQuery>();
            query.CustomerId = customerId;

            _mediator.Setup(m => m.Send(query, default))
                .ReturnsAsync(It.IsAny<List<Order>>());

            // Act
            var result = await _controller.GetOrderByCustomerId(customerId);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOkResult()
        {
            // Arrange
            var request = new CreateOrderRequestModel();
            var command = new CreateOrderCommand();
            var response = Guid.NewGuid();

            _mapper.Setup(m => m.Map<CreateOrderCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsBadRequestResult()
        {
            // Arrange
            var id = default(Guid);
            var request = AutoFaker.Generate<CreateOrderRequestModel>();
            var command = AutoFaker.Generate<CreateOrderCommand>();

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(id);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var okResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult()
        {
            // Arrange
            var request = new UpdateOrderRequestModel();
            var command = new UpdateOrderCommand();
            var response = true;

            _mapper.Setup(m => m.Map<UpdateOrderCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Update(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsBadRequestResult()
        {
            // Arrange
            var request = AutoFaker.Generate<UpdateOrderRequestModel>();
            var command = AutoFaker.Generate<UpdateOrderCommand>();
            var response = false;

            _mapper.Setup(m => m.Map<UpdateOrderCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Update(request);

            // Assert
            var okResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var request = new DeleteOrderRequestModel();
            var command = new DeleteOrderCommand();
            var response = true;

            _mapper.Setup(m => m.Map<DeleteOrderCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult()
        {
            // Arrange
            var request = AutoFaker.Generate<DeleteOrderRequestModel>();
            var command = AutoFaker.Generate<DeleteOrderCommand>();
            var response = false;

            _mapper.Setup(m => m.Map<DeleteOrderCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ChangeStatus_ReturnsOkResult()
        {
            // Arrange
            var request = new ChangeStatusRequestModel();
            var command = new ChangeStatusCommand();
            var response = true;

            _mapper.Setup(m => m.Map<ChangeStatusCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.ChangeStatus(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task ChangeStatus_ReturnsNotFoundResult()
        {
            // Arrange
            var request = AutoFaker.Generate<ChangeStatusRequestModel>();
            var command = AutoFaker.Generate<ChangeStatusCommand>();
            var response = false;

            _mapper.Setup(m => m.Map<ChangeStatusCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.ChangeStatus(request);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
