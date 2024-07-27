using MediatR;
using TesodevOrderApp.Shared.Domain.Models;

namespace Application.Commands
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Address Address { get; set; }
    }
}
