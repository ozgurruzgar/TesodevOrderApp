using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetAllOrderQuery : IRequest<List<Order>>
    {
    }
}
