using Domain.Args;
using Domain.Models;
using TesodevOrderApp.Shared.Domain.Models;
using TesodevOrderApp.Shared.Domain.Services;

namespace Domain.Services
{
    public interface ICustomerService : IDomainService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(Guid entityId);
        Task<Guid> CreateAsync(Customer customer);
        Task<bool> UpdateAsync(Customer Customer);
        Task<bool> DeleteAsync(Guid entityId);
        Task CreateOrderAsync(CreateOrderArgs args);
        Task UpdateAddressAsync(Customer customer, Address address);
    }
}
