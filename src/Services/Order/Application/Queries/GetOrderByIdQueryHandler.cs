using Domain.Models;
using Domain.Services;
using MediatR;

namespace Application.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetOrderByIdAsync(request.Id);
            return result;
        }
    }    
}
