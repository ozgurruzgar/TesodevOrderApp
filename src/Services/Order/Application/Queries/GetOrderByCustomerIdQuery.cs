using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetOrderByCustomerIdQuery : IRequest<List<Order>>
    {
        public Guid CustomerId { get; set; }
    }
}
