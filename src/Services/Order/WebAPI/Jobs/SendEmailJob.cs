using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Models.Dto_s;
using MediatR;
using WebAPI.Models;

namespace WebAPI.Jobs
{
    public class SendEmailJob
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public SendEmailJob(IMediator mediator, IMapper mapper, HttpClient httpClient)
        {
            _mediator = mediator;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task SendEmail()
        {
            var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>(ServiceApiSettings.CustomerServiceURL);
            if (customers == null)
                return;

            var query = new GetAllOrderQuery();
            var orders = await _mediator.Send(query);
            if (orders == null)
                return;

            foreach (var customer in customers) 
            {
                var customerOrders = orders
                    .Where(o => o.CustomerId == customer.Id)
                    .ToList();

                var command = new SendEmailCommand
                {
                    Email = customer.Email,
                    Name = customer.Name,
                    Orders = customerOrders
                };

                await _mediator.Send(command);
            }
        }
    }
}
