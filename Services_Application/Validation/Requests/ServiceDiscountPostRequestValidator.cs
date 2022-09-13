using FluentValidation;
using Services_Application.DTO.Requests;

namespace Services_Application.Validation.Requests
{
    public class ServiceDiscountPostRequestValidator : AbstractValidator<ServiceDiscountPostRequest>
    {
        public ServiceDiscountPostRequestValidator()
        {
            RuleFor(request => request.ServiceId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.ServiceId)}  can't be empty.");
            
            RuleFor(request => request.BranchId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.BranchId)}  can't be empty.");
            
            RuleFor(request => request.DiscountSize)
                .GreaterThan(0)
                .WithMessage(request => $"{nameof(request.DiscountSize)} value should be greater than 0.");
        }
    }
}