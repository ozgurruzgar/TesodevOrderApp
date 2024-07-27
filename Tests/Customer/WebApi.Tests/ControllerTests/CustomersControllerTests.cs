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

namespace WebApi.Tests.Controller
{
    public class CustomersControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IMapper> _mapper;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _mapper= new Mock<IMapper>();
            _controller = new CustomersController(_mediator.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllCustomer_ReturnsOkResult()
        {
            // Arrange
            var query = AutoFaker.Generate<GetAllCustomerQuery>();
            var expectedResponse = AutoFaker.Generate<List<Customer>>();

            _mediator.Setup(m => m.Send(It.IsAny<GetAllCustomerQuery>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAllCustomer();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsOkResult()
        {
            // Arrange
            var query = AutoFaker.Generate<GetCustomerByIdQuery>();
            var response = AutoFaker.Generate<Customer>();
            var mappedResponse = new GetCustomerByIdResponseModel(); 

            _mediator.Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), default))
                .ReturnsAsync(response);

            _mapper.Setup(m => m.Map<GetCustomerByIdResponseModel>(response))
                .Returns(mappedResponse);

            // Act
            var result = await _controller.GetCustomerById(query.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = AutoFaker.Generate<GetCustomerByIdQuery>();
            query.Id = id;

            _mediator.Setup(m => m.Send(query, default))
                .ReturnsAsync(It.IsAny<Customer>());

            // Act
            var result = await _controller.GetCustomerById(id);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOkResult()
        {
            // Arrange
            var request = new CreateCustomerRequestModel();
            var command = new CreateCustomerCommand(); 
            var response = Guid.NewGuid();

            _mapper.Setup(m => m.Map<CreateCustomerCommand>(request))
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
            var request = AutoFaker.Generate<CreateCustomerRequestModel>();
            var command = AutoFaker.Generate<CreateCustomerCommand>();

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
            var request = new UpdateCustomerRequestModel();
            var command = new UpdateCustomerCommand();
            var response = true;
            
            _mapper.Setup(m => m.Map<UpdateCustomerCommand>(request))
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
            var request = AutoFaker.Generate<UpdateCustomerRequestModel>();
            var command = AutoFaker.Generate<UpdateCustomerCommand>();
            var response = false;

            _mapper.Setup(m => m.Map<UpdateCustomerCommand>(request))
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
            var request = new DeleteCustomerRequestModel();
            var command = new DeleteCustomerCommand(); 
            var response = true; 

            _mapper.Setup(m => m.Map<DeleteCustomerCommand>(request))
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
            var request = AutoFaker.Generate<DeleteCustomerRequestModel>();
            var command = AutoFaker.Generate<DeleteCustomerCommand>();
            var response = false;

            _mapper.Setup(m => m.Map<DeleteCustomerCommand>(request))
                .Returns(command);

            _mediator.Setup(m => m.Send(command, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
