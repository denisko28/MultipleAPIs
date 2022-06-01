using System.Collections.Generic;
using MediatR;
using Services_Application.DTO.Responses;

namespace Services_Application.Queries.Services.GetAllServices
{
    public class GetAllServicesQuery : IRequest<IEnumerable<ServiceResponse>>
    {
        
    }
}