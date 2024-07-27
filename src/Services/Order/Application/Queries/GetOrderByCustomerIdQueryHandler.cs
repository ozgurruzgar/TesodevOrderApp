using Domain.Models;
using Domain.Services;
using MediatR;

namespace Application.Queries
{
    public class GetOrderByCustomerIdQueryHandler : IRequestHandler<GetOrderByCustomerIdQuery, List<Order>>
    {
        private readonly IOrderService _orderService;

        public GetOrderByCustomerIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<Order>> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderByCustomerIdAsync(request.CustomerId);
            return result;
        }
    }
}
