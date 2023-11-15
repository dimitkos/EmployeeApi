using FluentValidation;

namespace Application.Commands.Employees
{
    public class DeleteEmployeesValidator : AbstractValidator<DeleteEmployees>
    {
        public DeleteEmployeesValidator()
        {
            RuleFor(command => command.EmployeeIds).
                NotNull();
        }
    }
}
