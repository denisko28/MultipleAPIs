using FluentValidation;
using HR_BLL.DTO.Requests;

namespace HR_BLL.Validation.Requests
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