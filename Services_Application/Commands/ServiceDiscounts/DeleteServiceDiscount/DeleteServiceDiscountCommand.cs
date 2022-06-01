using MediatR;

namespace Services_Application.Commands.ServiceDiscounts.DeleteServiceDiscount
{
    public class DeleteServiceDiscountCommand: IRequest
    {
        public int Id { get; set; }
    }
}