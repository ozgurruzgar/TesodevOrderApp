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
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var query = new GetAllCustomerQuery();
            var response = await _mediator.Send(query);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            if (response == null)
                return NotFound();

            var mappedResponse = _mapper.Map<GetCustomerByIdResponseModel>(response);
            return Ok(mappedResponse);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCustomerRequestModel request)
        {
            var command = _mapper.Map<CreateCustomerCommand>(request);
            var response = await _mediator.Send(command);
            if (response == default)
                return BadRequest();

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCustomerRequestModel request)
        {
            var command = _mapper.Map<UpdateCustomerCommand>(request);
            var response = await _mediator.Send(command);
            if (!response)
                return BadRequest();

            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteCustomerRequestModel request)
        {
            var command = _mapper.Map<DeleteCustomerCommand>(request);
            var response = await _mediator.Send(command);
            if (!response)
                return NotFound();

            return Ok(response);
        }
    }
}
