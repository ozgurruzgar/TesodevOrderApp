using Domain.Models;

namespace Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<Order> orders, string name, string address);
    }
}
