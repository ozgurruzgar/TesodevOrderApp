using MediatR;
using TesodevOrderApp.Shared.Domain.Models;

namespace Application.Commands
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
