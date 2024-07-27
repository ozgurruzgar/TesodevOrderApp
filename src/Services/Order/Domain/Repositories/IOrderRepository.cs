using Domain.Models;
using TesodevOrderApp.Shared.Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<bool> ChangeStatusAsync(Order order);
        Task<List<Order>> GetByCustomerIdAsync(Guid customerId);
    }
}
