using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class BarberRegisterRequestValidator : AbstractValidator<BarberRegisterRequest>
    {
        public BarberRegisterRequestValidator()
        {
            Include(new EmployeeRegisterRequestValidator());

            RuleFor(request => request.ChairNum)
                .GreaterThan(0)
                .LessThanOrEqualTo(30)
                .WithMessage("The chair number should be greater than 0 and less than or equal to 30.");
        }
    }
}