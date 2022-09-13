using System;
using FluentValidation;
using HR_BLL.DTO.Requests;

namespace HR_BLL.Validation.Requests
{
    public class DayOffPostRequestValidator : AbstractValidator<DayOffPostRequest>
    {
        public DayOffPostRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)}  can't be empty.");
            
            RuleFor(request => request.EmployeeUserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.EmployeeUserId)}  can't be empty.");
            
            RuleFor(request => request.Date)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Date)}  can't be empty.")
                .GreaterThan(DateTime.Now)
                .WithMessage(request => $"{nameof(request.Date)} value should be date in the future.");
        }
    }
}