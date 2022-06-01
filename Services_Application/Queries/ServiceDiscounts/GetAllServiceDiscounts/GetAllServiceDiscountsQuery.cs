using System.Collections.Generic;
using MediatR;
using Services_Application.DTO.Responses;

namespace Services_Application.Queries.ServiceDiscounts.GetAllServiceDiscounts
{
    public class GetAllServiceDiscountsQuery : IRequest<IEnumerable<ServiceDiscountResponse>>
    {
        
    }
}