using AutoMapper;
using Common.Events.BranchEvents;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using MassTransit;

namespace Customers_BLL.EventBusConsumers.BranchConsumers;

public class BranchUpdatedConsumer : IConsumer<BranchUpdatedEvent>
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IBranchRepository _branchRepository;

    public BranchUpdatedConsumer(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _branchRepository = unitOfWork.BranchRepository;
    }

    public async Task Consume(ConsumeContext<BranchUpdatedEvent> context)
    {
        var branch = _mapper.Map<Branch>(context.Message);

        await _branchRepository.UpdateAsync(branch);
        await _unitOfWork.SaveChangesAsync();
    }
}