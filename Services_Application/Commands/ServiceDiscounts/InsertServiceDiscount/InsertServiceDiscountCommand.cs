using MediatR;
using Services_Application.DTO.Requests;

namespace Services_Application.Commands.ServiceDiscounts.InsertServiceDiscount
{
    public class InsertServiceDiscountCommand: IRequest
    {
        public ServiceDiscountRequest ServiceDiscountRequest { get; set; }
    }
}