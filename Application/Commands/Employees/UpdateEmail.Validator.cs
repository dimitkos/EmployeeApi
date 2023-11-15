using FluentValidation;

namespace Application.Commands.Employees
{
    public class UpdateEmailValidator : AbstractValidator<UpdateEmail>
    {
        public UpdateEmailValidator()
        {
            RuleFor(command => command.Payload.EmployeeId)
                .NotEmpty();
            RuleFor(command => command.Payload.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(30);
        }
    }
}
