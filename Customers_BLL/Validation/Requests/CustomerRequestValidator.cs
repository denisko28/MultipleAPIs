using System;
using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(request => request.UserId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.UserId)}  can't be empty.");
        }
    }
}