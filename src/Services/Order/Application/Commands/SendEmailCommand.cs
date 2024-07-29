using Domain.Models;
using MediatR;

namespace Application.Commands
{
    public class SendEmailCommand : IRequest<Unit>
    {
        public List<Order> Orders { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
