using Domain.Models;
using Domain.Services;
using MediatR;

namespace Application.Queries
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<Customer>>
    {
        private readonly ICustomerService _customerService;

        public GetAllCustomerQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Task<List<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = _customerService.GetAllAsync();
            return customers;
        }
    }
}
