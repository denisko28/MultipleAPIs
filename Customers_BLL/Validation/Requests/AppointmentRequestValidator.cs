using System;
using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class AppointmentRequestValidator : AbstractValidator<AppointmentRequest>
    {
        public AppointmentRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)}  can't be empty.");

            RuleFor(request => request.BarberUserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.BarberUserId)}  can't be empty.");

            RuleFor(request => request.CustomerUserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.CustomerUserId)}  can't be empty.");

            RuleFor(request => request.AppointmentStatusId)
                .GreaterThan(0)
                .WithMessage(request => $"{nameof(request.AppointmentStatusId)}  should be greater than 0.")
                .LessThan(5)
                .WithMessage(request => $"{nameof(request.AppointmentStatusId)}  should be less than 5.");

            RuleFor(request => request.AppDate)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.AppDate)}  can't be empty.")
                .GreaterThan(DateTime.Now)
                .WithMessage(request => $"{nameof(request.AppDate)}  should be date in the future.");

            RuleFor(request => request.BeginTime)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.BeginTime)}  can't be empty.");

            RuleFor(request => request.EndTime)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.EndTime)}  can't be empty.");

            RuleFor(request => request)
                .Must(CompareTime)
                .WithMessage(request => $"{nameof(request.BeginTime)} should be less than {nameof(request.EndTime)}.")
                .Must(CheckIfMultipleOf15)
                .WithMessage("Duration should be multiple of 15 minutes.");
        }

        private static bool CompareTime(AppointmentRequest request)
        {
            return request.BeginTime < request.EndTime;
        }

        private static bool CheckIfMultipleOf15(AppointmentRequest request)
        {
            return (request.BeginTime - request.EndTime).Minutes % 15 == 0;
        }
    }
}