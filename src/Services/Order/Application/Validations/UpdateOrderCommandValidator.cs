using Application.Commands;
using FluentValidation;

namespace Application.Validations
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();

            RuleFor(c => c.Quantity)
                .GreaterThan(0);

            RuleFor(c => c.Price)
                .GreaterThan(0);

            RuleFor(c => c.Address.AddressLine)
                .NotEmpty();

            RuleFor(c => c.Address.Country)
                .NotEmpty();            
            
            RuleFor(c => c.Address.City)
                .NotEmpty();

            RuleFor(c => c.Address.CityCode)
                .NotEmpty();
        }
    }
}
