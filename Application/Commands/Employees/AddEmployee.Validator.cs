using FluentValidation;

namespace Application.Commands.Employees
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployee>
    {
        public AddEmployeeValidator()
        {
            RuleFor(command => command.Payload.Name)
                .NotEmpty()
                .MaximumLength(25);
            RuleFor(command => command.Payload.Surname)
                .NotEmpty()
                .MaximumLength(25);
            RuleFor(command => command.Payload.Salary)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(command => command.Payload.Gender)
                .IsInEnum();
            RuleFor(command => command.Payload.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(30);
            RuleFor(command => command.Payload.IsManager)
                .NotNull();
            RuleFor(command => command.Payload.PhoneNumber.Phone)
                .NotEmpty();
            RuleFor(command => command.Payload.PhoneNumber.MobilePhone)
                .NotEmpty();
        }
    }
}
