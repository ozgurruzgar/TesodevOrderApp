using Domain.Args;
using Domain.Events;
using Domain.Exceptions;
using Domain.Services;
using MassTransit;
using TesodevOrderApp.Shared.Domain.Constants;

namespace Application.Consumers
{
    public class FillAddressMessageConsumer : IConsumer<FillAdressMessage>
    {
        private readonly ICustomerService _customerService;

        public FillAddressMessageConsumer(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task Consume(ConsumeContext<FillAdressMessage> context)
        {
            var customer = await _customerService.GetByIdAsync(context.Message.CustomerId) ?? throw new CustomerNotFoundException(Constants.ExceptionMessages.CustomerNotFound);
            var args = new CreateOrderArgs
            {
                Id = context.Message.Id,
                CustomerId = customer.Id,
                Price = context.Message.Price,
                Quantity = context.Message.Quantity,
                Status = context.Message.Status,
                Address = customer.Address,
                Product = context.Message.Product,
            };
            await _customerService.CreateOrderAsync(args);
        }
    }
}
