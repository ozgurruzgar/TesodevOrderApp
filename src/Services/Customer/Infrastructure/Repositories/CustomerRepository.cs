using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderAppDbContext _context;

        public CustomerRepository(OrderAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Customer entity)
        {
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(Customer entity)
        {
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var result = await _context.Customers.ToListAsync();
            return result;
        }

        public async Task<Customer> GetByIdAsync(Guid entityId)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.Id == entityId);
            return result;
        }

        public async Task<bool> UpdateAsync(Customer entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateAsync(Customer entity)
        {

            if (string.IsNullOrWhiteSpace(entity.Name))
                return false;

            if (string.IsNullOrWhiteSpace(entity.Email))
                return false;

            if (entity.Address == null)
                return false;

            if (string.IsNullOrWhiteSpace(entity.Email))
                return false;

            if (string.IsNullOrWhiteSpace(entity.Address.AddressLine))
                return false;

            if (string.IsNullOrWhiteSpace(entity.Address.Country))
                return false;

            if (string.IsNullOrWhiteSpace(entity.Address.City))
                return false;

            if (entity.Address.CityCode < 2 || entity.Address.CityCode == 0)
                return false;

            return true;
        }
    }
}
