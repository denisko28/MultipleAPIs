using AutoMapper;
using Common.Events.BranchEvents;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using MassTransit;

namespace Customers_BLL.EventBusConsumers.BranchConsumers;

public class BranchInsertedConsumer : IConsumer<BranchInsertedEvent>
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IBranchRepository _branchRepository;

    public BranchInsertedConsumer(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _branchRepository = unitOfWork.BranchRepository;
    }

    public async Task Consume(ConsumeContext<BranchInsertedEvent> context)
    {
        var branch = _mapper.Map<Branch>(context.Message);

        await _branchRepository.InsertAsync(branch);
        await _unitOfWork.SaveChangesAsync();
    }
}