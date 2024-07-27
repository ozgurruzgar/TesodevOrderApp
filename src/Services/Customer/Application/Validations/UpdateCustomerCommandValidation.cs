using Application.Commands;
using FluentValidation;

namespace Application.Validations
{
    public class UpdateCustomerCommandValidation : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty();

            RuleFor(c => c.Name)
                .NotEmpty();

            RuleFor(c => c.Email)
                .NotEmpty();

            RuleFor(c => c.Address)
                .NotNull();

            RuleFor(c => c.Address.AddressLine)
                .NotEmpty();

            RuleFor(c => c.Address.City)
                .NotEmpty();

            RuleFor(c => c.Address.Country)
                .NotEmpty();

            RuleFor(c => c.Address.CityCode)
                .NotEmpty();
        }
    }
}
