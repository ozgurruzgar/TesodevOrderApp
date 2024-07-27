using AutoMapper;
using Domain.Args;
using Domain.Services;
using MediatR;

namespace Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var args = _mapper.Map<FillAddressArgs>(request);
            var result = await _orderService.FillAddressAsync(args);
            return result;
        }
    }
}
