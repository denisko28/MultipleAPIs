using AutoMapper;
using Common.Events.BranchEvents;
using MassTransit;
using MediatR;
using Services_Application.Commands.Branches.UpdateBranch;
using Services_Application.DTO.Requests;

namespace Services_Application.EventBusConsumers.BranchConsumers;

public class BranchUpdatedConsumer : IConsumer<BranchUpdatedEvent>
{
    private readonly IMapper _mapper;
    
    private readonly IMediator _mediator;

    public BranchUpdatedConsumer(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<BranchUpdatedEvent> context)
    {
        var branch = _mapper.Map<BranchRequest>(context.Message);
        await _mediator.Send(new UpdateBranchCommand{ BranchRequest = branch});
    }
}