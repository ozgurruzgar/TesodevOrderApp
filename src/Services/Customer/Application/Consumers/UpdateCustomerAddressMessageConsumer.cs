using Domain.Exceptions;
using Domain.Services;
using MassTransit;
using TesodevOrderApp.Shared.Domain.Constants;
using TesodevOrderApp.Shared.Domain.Messages;

namespace Application.Consumers
{
    public class UpdateCustomerAddressMessageConsumer : IConsumer<UpdateCustomerAddressMessage>
    {
        private readonly ICustomerService _customerService;

        public UpdateCustomerAddressMessageConsumer(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task Consume(ConsumeContext<UpdateCustomerAddressMessage> context)
        {
            var customer = await _customerService.GetByIdAsync(context.Message.CustomerId) ?? throw new CustomerNotFoundException(Constants.ExceptionMessages.CustomerNotFound);
            customer.Address = context.Message.Address;
            await _customerService.UpdateAddressAsync(customer, context.Message.Address);
        }
    }
}
