using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderService _orderService;

        public DeleteOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var result = _orderService.DeleteAsync(request.Id);
            return result;
        }
    }
}
