using FluentValidation;
using Services_Application.DTO.Requests;

namespace Services_Application.Validation.Requests
{
    public class ServicePostRequestValidator : AbstractValidator<ServicePostRequest>
    {
        public ServicePostRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Name)}  can't be empty.")
                .MaximumLength(30)
                .WithMessage(request => $"{nameof(request.Name)} value should be shorter than 30 characters");
            
            RuleFor(request => request.Duration)
                .GreaterThanOrEqualTo(15)
                .WithMessage(request => $"{nameof(request.Duration)} value should be greater than or equal to 15");
            
            RuleFor(request => request.Price)
                .GreaterThan(0)
                .WithMessage(request => $"{nameof(request.Price)} value should be greater than 0.");
        }
    }
}