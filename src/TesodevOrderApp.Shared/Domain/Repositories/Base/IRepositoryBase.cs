using Microsoft.EntityFrameworkCore;
using TesodevOrderApp.Shared.Domain.Models.Base;

namespace TesodevOrderApp.Shared.Domain.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class, IAggregateRoot, new()
    {
        Task<Guid> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid entityId);
    }
}
