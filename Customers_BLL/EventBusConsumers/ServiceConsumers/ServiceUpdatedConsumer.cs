using AutoMapper;
using Common.Events.ServiceEvents;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using MassTransit;

namespace Customers_BLL.EventBusConsumers.ServiceConsumers;

public class ServiceUpdatedConsumer : IConsumer<ServiceUpdatedEvent>
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IServiceRepository _serviceRepository;

    public ServiceUpdatedConsumer(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _serviceRepository = unitOfWork.ServiceRepository;
    }

    public async Task Consume(ConsumeContext<ServiceUpdatedEvent> context)
    {
        var service = _mapper.Map<Service>(context.Message);

        await _serviceRepository.UpdateAsync(service);
        await _unitOfWork.SaveChangesAsync();
    }
}