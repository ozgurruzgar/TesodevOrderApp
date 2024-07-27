using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetAllCustomerQuery : IRequest<List<Customer>>
    {
    }
}
