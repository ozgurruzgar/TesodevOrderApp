using Domain.Models;
using Domain.Services;
using MediatR;

namespace Application.Queries
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
    {
        private readonly IOrderService _orderService;

        public GetAllOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetAllAsync();
            return result;
        }
    }
}
