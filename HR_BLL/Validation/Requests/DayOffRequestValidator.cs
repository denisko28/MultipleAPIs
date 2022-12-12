using System;
using FluentValidation;
using HR_BLL.DTO.Requests;

namespace HR_BLL.Validation.Requests
{
    public class DayOffRequestValidator : AbstractValidator<DayOffRequestDto>
    {
        public DayOffRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)}  can't be empty.");
            
            RuleFor(request => request.Date)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Date)}  can't be empty.");
        }
    }
}