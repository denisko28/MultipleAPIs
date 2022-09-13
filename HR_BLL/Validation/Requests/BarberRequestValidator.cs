using System;
using System.Threading.Tasks;
using FluentValidation;
using HR_BLL.DTO.Requests;

namespace HR_BLL.Validation.Requests
{
    public class BarberRequestValidator : AbstractValidator<BarberRequest>
    {
        public BarberRequestValidator()
        {
            RuleFor(request => request.EmployeeUserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.EmployeeUserId)}  can't be empty.");

            RuleFor(request => request.ChairNum)
                .GreaterThan(0)
                .LessThanOrEqualTo(30)
                .WithMessage("The chair number should be greater than 0 and less than or equal to 30.");
        }
    }
}