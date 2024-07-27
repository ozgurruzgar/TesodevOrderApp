using Domain.Args;
using Domain.Models;
using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderService _orderService;

        public UpdateOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new UpdateOrderArgs
            {
                Id = request.Id,
                Quantity = request.Quantity,
                Price =  request.Price,
                Address = request.Address,
            };

            var result = _orderService.UpdateAsync(order);
            return result;
        }
    }
}
