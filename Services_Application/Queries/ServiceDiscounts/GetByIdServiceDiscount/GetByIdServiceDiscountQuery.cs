using MediatR;
using Services_Application.DTO.Responses;

namespace Services_Application.Queries.ServiceDiscounts.GetByIdServiceDiscount
{
    public class GetByIdServiceDiscountQuery : IRequest<ServiceDiscountResponse>
    {
        public int Id { get; set; }
    }
}