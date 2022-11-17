using MediatR;
using Services_Application.DTO.Requests;

namespace Services_Application.Commands.Branches.InsertBranch
{
    public class InsertBranchCommand: IRequest
    {
        public BranchRequest BranchRequest { get; set; }
    }
}