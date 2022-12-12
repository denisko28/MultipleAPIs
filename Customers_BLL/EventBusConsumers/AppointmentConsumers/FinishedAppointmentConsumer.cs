using AutoMapper;
using Common.Events.AppointmentEvents;
using Customers_BLL.Protos;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using MassTransit;

namespace Customers_BLL.EventBusConsumers.AppointmentConsumers;

public class FinishedAppointmentConsumer : IConsumer<FinishedAppointmentEvent>
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IAppointmentRepository _appointmentRepository;

    private readonly IAppointmentServiceRepository _appointmentServiceRepository;
    
    private readonly Employees.EmployeesClient _employeesClient;

    public FinishedAppointmentConsumer(IMapper mapper, IUnitOfWork unitOfWork, Employees.EmployeesClient employeesClient)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _appointmentRepository = unitOfWork.AppointmentRepository;
        _appointmentServiceRepository = unitOfWork.AppointmentServiceRepository;
        _employeesClient = employeesClient;
    }

    public async Task Consume(ConsumeContext<FinishedAppointmentEvent> context)
    {
        var appointment = _mapper.Map<Appointment>(context.Message);
        var employeeResp = 
            _employeesClient.GetById(new GetEmployeeByIdRequest{ Id = appointment.BarberUserId });
        appointment.BranchId = employeeResp.BranchId;
        appointment.AppointmentStatusId = 0;
        await _appointmentRepository.InsertAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        var insertedId = appointment.Id;
        var appServices = context.Message.ServiceIds
            .Select(serviceId => 
                new AppointmentService {AppointmentId = insertedId, ServiceId = serviceId}
            ).ToList();
        await _appointmentServiceRepository.InsertRangeAsync(appServices);

        await _unitOfWork.SaveChangesAsync();
    }
}