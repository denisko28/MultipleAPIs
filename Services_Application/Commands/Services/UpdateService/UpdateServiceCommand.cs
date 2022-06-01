using MediatR;
using Services_Application.DTO.Requests;

namespace Services_Application.Commands.Services.UpdateService
{
    public class UpdateServiceCommand: IRequest
    {
        public ServiceRequest ServiceRequest { get; set; }
    }
}