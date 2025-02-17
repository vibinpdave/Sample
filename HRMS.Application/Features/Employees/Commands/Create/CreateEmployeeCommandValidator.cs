
using FluentValidation;

namespace HRMS.Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("First Name is required.")
           .Length(2, 100).WithMessage("First Name must be between 2 and 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, 50).WithMessage("Password must be between 6 and 50 characters.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status must be a valid value.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City required.");
            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State required.");
        }
    }
}
