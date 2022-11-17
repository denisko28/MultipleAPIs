using Common.Events.ServiceEvents;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using MassTransit;

namespace Customers_BLL.EventBusConsumers.ServiceConsumers;

public class ServiceDeletedConsumer : IConsumer<ServiceDeletedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IServiceRepository _serviceRepository;

    public ServiceDeletedConsumer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _serviceRepository = unitOfWork.ServiceRepository;
    }

    public async Task Consume(ConsumeContext<ServiceDeletedEvent> context)
    {
        var ServiceId = context.Message.Id;

        await _serviceRepository.DeleteByIdAsync(ServiceId);
        await _unitOfWork.SaveChangesAsync();
    }
}