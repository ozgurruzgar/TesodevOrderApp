using Application.Commands;
using FluentValidation;

namespace Application.Validations
{
    public class ChangeStatusCommandValidator : AbstractValidator<ChangeStatusCommand>
    {
        public ChangeStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();

            RuleFor(c => c.Status)
                .NotEmpty();
        }
    }
}
