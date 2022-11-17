using AutoMapper;
using Common.Events.BranchEvents;
using MassTransit;
using MediatR;
using Services_Application.Commands.Branches.InsertBranch;
using Services_Application.DTO.Requests;

namespace Services_Application.EventBusConsumers.BranchConsumers;

public class BranchInsertedConsumer : IConsumer<BranchInsertedEvent>
{
    private readonly IMapper _mapper;
    
    private readonly IMediator _mediator;

    public BranchInsertedConsumer(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<BranchInsertedEvent> context)
    {
        var branch = _mapper.Map<BranchRequest>(context.Message);
        await _mediator.Send(new InsertBranchCommand{ BranchRequest = branch});
    }
}