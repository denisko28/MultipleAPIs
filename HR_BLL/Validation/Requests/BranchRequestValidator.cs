using FluentValidation;
using HR_BLL.DTO.Requests;

namespace HR_BLL.Validation.Requests
{
    public class BranchRequestValidator : AbstractValidator<BranchRequest>
    {
        public BranchRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)}  can't be empty.");
            
            RuleFor(request => request.Descript)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Descript)}  can't be empty.")
                .MaximumLength(20)
                .WithMessage(request => $"{nameof(request.Address)} value should be shorter than 20 characters");
            
            RuleFor(request => request.Address)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Address)}  can't be empty.")
                .MaximumLength(80)
                .WithMessage(request => $"{nameof(request.Address)} value should be shorter than 80 characters");
        }
    }
}