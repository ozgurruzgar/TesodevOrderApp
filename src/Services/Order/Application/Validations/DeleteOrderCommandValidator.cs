using Application.Commands;
using FluentValidation;

namespace Application.Validations
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
