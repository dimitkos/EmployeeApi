using FluentValidation;

namespace Application.Commands.Employees
{
    public class PromoteValidator : AbstractValidator<Promote>
    {
        public PromoteValidator()
        {
            RuleFor(command => command.EmployeeId)
                .NotEmpty();
        }
    }
}
