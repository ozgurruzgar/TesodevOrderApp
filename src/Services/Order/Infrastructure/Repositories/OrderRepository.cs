using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using TesodevOrderApp.Shared.Domain.Models.Base;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderAppDbContext _context;

        public OrderRepository(OrderAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Order entity)
        {
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(Order entity)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Order> GetByIdAsync(Guid entityId)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(c => c.Id == entityId);
            return result;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var result = await _context.Orders.ToListAsync();
            return result;
        }

        public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId)
        {
            var result = await _context.Orders
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> ChangeStatusAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
