using Domain.Args;
using Domain.Events;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using MassTransit;
using TesodevOrderApp.Shared.Domain.Constants;
using TesodevOrderApp.Shared.Domain.Messages;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OrderService(IOrderRepository orderRepository, ISendEndpointProvider sendEndpointProvider)
        {
            _orderRepository = orderRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<bool> ChangeStatusAsync(Guid orderId, string status)
        {
            if (orderId == default)
                return false;

            if (string.IsNullOrWhiteSpace(status))
                return false;

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return false;
            else
                order.Status = status;

            var result = await _orderRepository.ChangeStatusAsync(order);
            return result;
        }

        public async Task CreateAsync(Order order)
        {
            var validatedOrder = await ValidateAsync(order);
            if (validatedOrder == false)
                throw new InvalidOrderException(Constants.ExceptionMessages.InvalidOrder);

            order.Product.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;

            var result = await _orderRepository.CreateAsync(order);
        }

        public async Task<bool> DeleteAsync(Guid orderId)
        {
            var entity = await _orderRepository.GetByIdAsync(orderId);
            if (entity == null)
                return false;
            else
            {
                var result = _orderRepository.DeleteAsync(entity);
                return true;
            }
        }

        public async Task<Guid> FillAddressAsync(FillAddressArgs args)
        {
            if (args.CustomerId == default)
                throw new ArgumentException(null, nameof(args.CustomerId));

            var uri = new Uri(Constants.Queues.FillAddressQueueName);
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(uri);

            var message = new FillAdressMessage 
            {
                Id = Guid.NewGuid(),
                CustomerId = args.CustomerId,
                Price = args.Price,
                Quantity = args.Quantity,
                Status = args.Status,
                Product = args.Product
            };
            await sendEndpoint.Send(message);
            return message.Id;
        }

        public Task<List<Order>> GetAllAsync()
        {
            var result = _orderRepository.GetAllAsync();
            return result;
        }

        public Task<List<Order>> GetOrderByCustomerIdAsync(Guid customerId)
        {
            var result = _orderRepository.GetByCustomerIdAsync(customerId);
            return result;
        }

        public Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            var result = _orderRepository.GetByIdAsync(orderId);
            return result;
        }

        public async Task<bool> UpdateAsync(UpdateOrderArgs args)
        {
            var currentOrder = await _orderRepository.GetByIdAsync(args.Id);
            if (currentOrder == null)
                return false;

            currentOrder.Address = args.Address;
            currentOrder.Quantity = args.Quantity;
            currentOrder.Price = args.Price;
            currentOrder.UpdatedAt = DateTime.UtcNow;

            var validatedOrder = await ValidateAsync(currentOrder);
            if (validatedOrder == false)
                return false;

            var uri = new Uri(Constants.Queues.UpdateCustomerAddressQueueName);
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(uri);

            var message = new UpdateCustomerAddressMessage
            {
                CustomerId = currentOrder.CustomerId,
                Address = currentOrder.Address
            };
            await sendEndpoint.Send(message);

            var result = await _orderRepository.UpdateAsync(currentOrder);
            return result;
        }

        private async Task<bool> ValidateAsync(Order order)
        {
            if (order.Quantity == 0)
                return false;

            if (order.Price == 0)
                return false;

            if (string.IsNullOrWhiteSpace(order.Status))
                return false;

            if (order.Address == null)
                return false;

            if (string.IsNullOrWhiteSpace(order.Address.AddressLine))
                return false;

            if (string.IsNullOrWhiteSpace(order.Address.Country))
                return false;

            if (string.IsNullOrWhiteSpace(order.Address.City))
                return false;

            if (order.Address.CityCode < 2 || order.Address.CityCode == 0)
                return false;

            if (string.IsNullOrWhiteSpace(order.Product.Name))
                return false;

            if (string.IsNullOrWhiteSpace(order.Product.ImageUrl))
                return false;

            return true;
        }
    }
}
