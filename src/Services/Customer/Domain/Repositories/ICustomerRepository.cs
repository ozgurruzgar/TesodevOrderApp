using Domain.Models;
using TesodevOrderApp.Shared.Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<bool> ValidateAsync(Customer entity);
    }
}
