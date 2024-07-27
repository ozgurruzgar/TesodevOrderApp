using MediatR;
using TesodevOrderApp.Shared.Domain.Models;

namespace Application.Commands
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
