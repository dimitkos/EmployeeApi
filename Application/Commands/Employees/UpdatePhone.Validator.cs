using FluentValidation;

namespace Application.Commands.Employees
{
    public class UpdatePhoneValidator : AbstractValidator<UpdatePhone>
    {
        public UpdatePhoneValidator()
        {
            RuleFor(command => command.Payload.EmployeeId)
                .NotEmpty();
            RuleFor(command => command.Payload.PhoneNumber.Phone)
                .NotEmpty();
            RuleFor(command => command.Payload.PhoneNumber.MobilePhone)
                .NotEmpty();
        }
    }
}
