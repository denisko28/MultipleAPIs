using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class BaseRegisterRequestValidator : AbstractValidator<BaseRegisterRequest>
    {
        public BaseRegisterRequestValidator()
        {
            RuleFor(request => request.Email)
                .EmailAddress().WithMessage("Incorrect Email value.");

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(6).WithMessage("Your password length must be at least 6.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

            RuleFor(request => request.PhoneNumber)
                .Matches(@"\+380[0-9]{9}")
                .WithMessage("Your phone number should begin wit +380 and end with 9 digits.");
            
            RuleFor(request => request.FirstName)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.FirstName)} can't be empty.")
                .MaximumLength(15)
                .WithMessage(request => $"{nameof(request.FirstName)} should be less than 15 characters.");

            RuleFor(request => request.LastName)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.LastName)} can't be empty.")
                .MaximumLength(15)
                .WithMessage(request => $"{nameof(request.LastName)} should be less than 15 characters.");
        }
    }
}