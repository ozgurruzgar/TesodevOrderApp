using Domain.Args;
using Domain.Events;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repositories;
using MassTransit;
using TesodevOrderApp.Shared.Domain.Constants;
using TesodevOrderApp.Shared.Domain.Models;

namespace Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CustomerService(ICustomerRepository customerRepository, ISendEndpointProvider sendEndpointProvider)
        {
            _customerRepository = customerRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public Task<List<Customer>> GetAllAsync()
        {
            var result = _customerRepository.GetAllAsync();
            return result;
        }

        public Task<Customer> GetByIdAsync(Guid entityId)
        {
            var result = _customerRepository.GetByIdAsync(entityId);
            return result;
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            var validatedCustomer = await _customerRepository.ValidateAsync(customer);
            if (validatedCustomer == false)
                throw new InvalidCustomerException("Invalid Customer.");

            customer.Id = Guid.NewGuid();
            customer.CreatedAt = DateTime.UtcNow;

            var result = await _customerRepository.CreateAsync(customer);
            return result;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            var updatedCustomer = await _customerRepository.GetByIdAsync(customer.Id);
            if (updatedCustomer == null)
                return false;

            updatedCustomer.Name = customer.Name;
            updatedCustomer.Email = customer.Email;
            updatedCustomer.Address = customer.Address;
            updatedCustomer.UpdatedAt = DateTime.UtcNow;

            var validatedCustomer = await _customerRepository.ValidateAsync(updatedCustomer);
            if (validatedCustomer == false)
                return false;

            var result = await _customerRepository.UpdateAsync(updatedCustomer);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid entityId)
        {
            var entity = await _customerRepository.GetByIdAsync(entityId);
            if (entity == null)
                return false;
            else
            {
                var result = _customerRepository.DeleteAsync(entity);
                return true;
            }
        }

        public async Task CreateOrderAsync(CreateOrderArgs args)
        {
            var uri = new Uri(Constants.Queues.SendAddressQueueName);
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(uri);

            var message = new SendAdressMessage
            {
                Id = args.Id,
                CustomerId = args.CustomerId,
                Quantity = args.Quantity,
                Price = args.Price,
                Status = args.Status,
                Product = args.Product,
                Address = args.Address
            };

            await sendEndpoint.Send(message);
        }

        public async Task UpdateAddressAsync(Customer customer, Address address)
        {
            customer.Address = address;
            await _customerRepository.UpdateAsync(customer);
        }
    }
}
