using MediatR;

namespace Application.Commands
{
    public class ChangeStatusCommand : IRequest<bool> 
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
