using FluentValidation;

namespace Application.Commands.Employees
{
    public class UpdateSalaryValidator : AbstractValidator<UpdateSalary>
    {
        public UpdateSalaryValidator()
        {
            RuleFor(command => command.Payload.EmployeeId)
                .NotEmpty();
            RuleFor(command => command.Payload.Salary)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
