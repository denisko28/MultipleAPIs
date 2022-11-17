using MediatR;

namespace Services_Application.Commands.Branches.DeleteBranch
{
    public class DeleteBranchCommand: IRequest
    {
        public int Id { get; set; }
    }
}