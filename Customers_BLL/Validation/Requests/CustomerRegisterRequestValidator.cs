using System;
using Customers_BLL.DTO.Requests;
using FluentValidation;

namespace Customers_BLL.Validation.Requests
{
    public class CustomerRegisterRequestValidator : AbstractValidator<CustomerRegisterRequest>
    {
        public CustomerRegisterRequestValidator()
        {
            Include(new BaseRegisterRequestValidator());
        }
    }
}