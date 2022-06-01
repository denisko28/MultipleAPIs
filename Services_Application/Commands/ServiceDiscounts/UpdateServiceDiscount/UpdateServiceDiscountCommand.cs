using MediatR;
using Services_Application.DTO.Requests;

namespace Services_Application.Commands.ServiceDiscounts.UpdateServiceDiscount
{
    public class UpdateServiceDiscountCommand: IRequest
    {
        public ServiceDiscountRequest ServiceDiscountRequest { get; set; }
    }
}