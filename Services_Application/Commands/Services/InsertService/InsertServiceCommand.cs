using MediatR;
using Services_Application.DTO.Requests;

namespace Services_Application.Commands.Services.InsertService
{
    public class InsertServiceCommand: IRequest
    {
        public ServiceRequest ServiceRequest { get; set; }
    }
}