using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, bool>
    {
        private readonly IOrderService _orderService;

        public ChangeStatusCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Task<bool> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            var result = _orderService.ChangeStatusAsync(request.Id, request.Status);
            return result;
        }
    }
}
