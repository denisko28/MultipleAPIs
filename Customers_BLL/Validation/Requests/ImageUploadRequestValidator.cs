using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class ImageUploadRequestValidator : AbstractValidator<ImageUploadRequest>
    {
        public ImageUploadRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)}  can't be empty.");
            
            RuleFor(request => request.Image)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Image)}  can't be empty.");
        }
    }
}