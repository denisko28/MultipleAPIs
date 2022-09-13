using System;
using FluentValidation;
using HR_BLL.DTO.Requests;

namespace HR_BLL.Validation.Requests
{
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeRequestValidator()
        {
            RuleFor(request => request.UserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.UserId)}  can't be empty.");

            RuleFor(request => request.BranchId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.BranchId)} can't be empty.");

            RuleFor(request => request.EmployeeStatusId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.EmployeeStatusId)} can't be empty.");

            RuleFor(request => request.Address)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Address)} can't be empty.")
                .MaximumLength(80)
                .WithMessage(request => $"{nameof(request.Address)} should be less than 80 characters.");

            RuleFor(request => request.Birthday)
                .NotEmpty()
                .LessThan(DateTime.Now);
        }
    }
}