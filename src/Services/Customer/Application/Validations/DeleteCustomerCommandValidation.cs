using Application.Commands;
using FluentValidation;

namespace Application.Validations
{
    public class DeleteCustomerCommandValidation : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
