using Common.Events.BranchEvents;
using MassTransit;
using MediatR;
using Services_Application.Commands.Branches.DeleteBranch;

namespace Services_Application.EventBusConsumers.BranchConsumers;

public class BranchDeletedConsumer : IConsumer<BranchDeletedEvent>
{
    private readonly IMediator _mediator;

    public BranchDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<BranchDeletedEvent> context)
    {
        var branchId = context.Message.Id;

        await _mediator.Send(new DeleteBranchCommand{ Id = branchId });
    }
}