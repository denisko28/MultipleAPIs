using MediatR;
using Services_Application.DTO.Requests;

namespace Services_Application.Commands.Branches.UpdateBranch
{
    public class UpdateBranchCommand: IRequest
    {
        public BranchRequest BranchRequest { get; set; }
    }
}