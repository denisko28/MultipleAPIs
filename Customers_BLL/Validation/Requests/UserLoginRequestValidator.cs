using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(request => request.Email)
                .EmailAddress()
                .WithMessage("Incorrect Email value.");

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage("Your password cannot be empty");
        }
    }
}