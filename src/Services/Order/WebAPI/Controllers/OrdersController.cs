using Application.Commands;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Request;
using WebAPI.Models.Response;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var query = new GetAllOrderQuery();
            var response = await _mediator.Send(query);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            if (response == null)
                return NotFound();

            var mappedResponse = _mapper.Map<GetOrderByIdResponseModel>(response);
            return Ok(mappedResponse);
        }

        [HttpGet("getbycustomerid/{customerId}")]
        public async Task<IActionResult> GetOrderByCustomerId(Guid customerId)
        {
            var query = new GetOrderByCustomerIdQuery { CustomerId = customerId };
            var response = await _mediator.Send(query);
            if (response == null)
                return NotFound();

            var mappedResponse = _mapper.Map<GetOrderByCustomerIdResponseModel>(response);
            return Ok(mappedResponse);
        }

        [HttpPost("changestatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusRequestModel request)
        {
            var command = _mapper.Map<ChangeStatusCommand>(request);
            var response = await _mediator.Send(command);
            if (!response)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrderRequestModel request)
        {
            var command = _mapper.Map<CreateOrderCommand>(request);
            var response = await _mediator.Send(command);
            if (response == default)
                return BadRequest();

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateOrderRequestModel request)
        {
            var command = _mapper.Map<UpdateOrderCommand>(request);
            var response = await _mediator.Send(command);
            if (!response)
                return BadRequest();

            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteOrderRequestModel request)
        {
            var command = _mapper.Map<DeleteOrderCommand>(request);
            var response = await _mediator.Send(command);
            if (!response)
                return NotFound();

            return Ok(response);
        }
    }
}
