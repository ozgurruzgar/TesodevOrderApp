using Application.Queries;
using FluentValidation;

namespace Application.Validations
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
