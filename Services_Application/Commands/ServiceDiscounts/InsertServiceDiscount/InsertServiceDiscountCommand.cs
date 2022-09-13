using MediatR;
using Services_Application.DTO.Requests;

namespace Services_Application.Commands.ServiceDiscounts.InsertServiceDiscount
{
    public class InsertServiceDiscountCommand: IRequest
    {
        public ServiceDiscountPostRequest ServiceDiscountPostRequest { get; set; }
    }
}