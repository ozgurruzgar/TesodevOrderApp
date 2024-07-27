using Domain.Args;
using Domain.Models;
using TesodevOrderApp.Shared.Domain.Services;

namespace Domain.Services
{
    public interface IOrderService : IDomainService
    {
        Task CreateAsync(Order order);
        Task<bool> UpdateAsync(UpdateOrderArgs args);
        Task<bool> DeleteAsync(Guid orderId);
        Task<bool> ChangeStatusAsync(Guid orderId, string status);
        Task<List<Order>> GetAllAsync();
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<List<Order>> GetOrderByCustomerIdAsync(Guid customerId);
        Task<Guid> FillAddressAsync(FillAddressArgs args);
    }
}
