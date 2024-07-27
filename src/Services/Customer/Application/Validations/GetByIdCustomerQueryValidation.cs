using Application.Queries;
using FluentValidation;

namespace Application.Validations
{
    public class GetCustomerByIdQueryValidation : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
