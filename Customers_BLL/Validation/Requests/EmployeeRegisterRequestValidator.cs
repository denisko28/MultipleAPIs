using System;
using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class EmployeeRegisterRequestValidator : AbstractValidator<EmployeeRegisterRequest>
    {
        public EmployeeRegisterRequestValidator()
        {
            Include(new BaseRegisterRequestValidator());

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