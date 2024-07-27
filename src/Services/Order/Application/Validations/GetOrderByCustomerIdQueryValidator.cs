using Application.Queries;
using FluentValidation;

namespace Application.Validations
{
    public class GetOrderByCustomerIdQueryValidator : AbstractValidator<GetOrderByCustomerIdQuery>
    {
        public GetOrderByCustomerIdQueryValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotEmpty();
        }
    }
}
