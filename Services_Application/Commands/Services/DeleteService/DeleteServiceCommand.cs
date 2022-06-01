using MediatR;

namespace Services_Application.Commands.Services.DeleteService
{
    public class DeleteServiceCommand: IRequest
    {
        public int Id { get; set; }
    }
}