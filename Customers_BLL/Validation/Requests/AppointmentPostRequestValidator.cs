using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class AppointmentPostRequestValidator : AbstractValidator<AppointmentPostRequest>
    {
        public AppointmentPostRequestValidator()
        {
            RuleFor(request => request.BarberUserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.BarberUserId)}  can't be empty.");

            RuleFor(request => request.CustomerUserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.CustomerUserId)}  can't be empty.");

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

            RuleFor(request => request.ServiceIds)
                .NotEmpty();

            RuleFor(request => request)
                .Must(CompareTime)
                .WithMessage(request => $"{nameof(request.BeginTime)} should be less than {nameof(request.EndTime)}.")
                .Must(CheckIfMultipleOf15)
                .WithMessage("Duration should be multiple of 15 minutes.")
                .WithMessage("Appointment can not have unavailable services");
        }

        private static bool CompareTime(AppointmentPostRequest request)
        {
            return request.BeginTime < request.EndTime;
        }

        private static bool CheckIfMultipleOf15(AppointmentPostRequest request)
        {
            return (request.BeginTime - request.EndTime).Minutes % 15 == 0;
        }
    }
}