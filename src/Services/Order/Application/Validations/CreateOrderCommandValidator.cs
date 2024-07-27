using Application.Commands;
using FluentValidation;

namespace Application.Validations
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
            RuleFor(c => c.Quantity)
                .GreaterThan(0);

            RuleFor(c => c.Price)
                .GreaterThan(0);

            RuleFor(c => c.Status)
                .NotEmpty();

            RuleFor(c => c.Product)
                .NotEmpty();
        }
    }
}
