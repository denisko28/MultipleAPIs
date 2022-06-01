using MediatR;
using Services_Application.DTO.Responses;

namespace Services_Application.Queries.Services.GetByIdService
{
    public class GetByIdServiceQuery : IRequest<ServiceResponse>
    {
        public int Id { get; set; }
    }
}