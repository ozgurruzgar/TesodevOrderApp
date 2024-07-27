using Domain.Events;
using Domain.Models;
using Domain.Services;
using MassTransit;
using System.Text.Json;

namespace Application.Consumers
{
    public class SendAddressMessageConsumer : IConsumer<SendAdressMessage>
    {
        private readonly IOrderService _orderService;

        public SendAddressMessageConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<SendAdressMessage> context)
        {
            var order = new Order
            {
                Id = context.Message.Id,
                CustomerId = context.Message.CustomerId,
                Quantity = context.Message.Quantity,
                Price = context.Message.Price,
                Status = context.Message.Status,
                Address = context.Message.Address,
                Product = JsonSerializer.Deserialize<Product>(context.Message.Product)
            };
            await _orderService.CreateAsync(order);
        }
    }
}
