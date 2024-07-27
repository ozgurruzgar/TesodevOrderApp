using Domain.Models;
using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerService _customerService;

        public UpdateCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
            };

            var result = _customerService.UpdateAsync(customer);
            return result;
        }
    }
}
