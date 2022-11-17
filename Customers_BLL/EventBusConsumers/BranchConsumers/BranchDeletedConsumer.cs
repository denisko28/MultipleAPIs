using Common.Events.BranchEvents;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using MassTransit;

namespace Customers_BLL.EventBusConsumers.BranchConsumers;

public class BranchDeletedConsumer : IConsumer<BranchDeletedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IBranchRepository _branchRepository;

    public BranchDeletedConsumer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _branchRepository = unitOfWork.BranchRepository;
    }

    public async Task Consume(ConsumeContext<BranchDeletedEvent> context)
    {
        var branchId = context.Message.Id;

        await _branchRepository.DeleteByIdAsync(branchId);
        await _unitOfWork.SaveChangesAsync();
    }
}