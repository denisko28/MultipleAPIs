using AutoMapper;
using Customers_BLL.Protos;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Grpc.Core;

namespace Customers_BLL.Grpc;

public class AppointmentsService : Appointments.AppointmentsBase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;

    public AppointmentsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _appointmentRepository = unitOfWork.AppointmentRepository;
        _mapper = mapper;
    }

    public override async Task<AppointmentsResponse> GetAll(GetAllAppointmentsRequest request,
        ServerCallContext context)
    {
        try
        {
            var appointmentsResponse = new AppointmentsResponse();
            var appointments = await _appointmentRepository.GetAllAsync();
            var data =
                appointments.Select(_mapper.Map<Customers_DAL.Entities.Appointment, AppointmentResponse>);
            appointmentsResponse.Data.AddRange(data);
            return appointmentsResponse;
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }

    public override async Task<AppointmentResponse> GetById(GetAppointmentByIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            return _mapper.Map<Customers_DAL.Entities.Appointment, AppointmentResponse>(appointment);
        }
        catch (EntityNotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }

    public override async Task<AppointmentsResponse> GetByDateAndBarber(GetByDateAndBarberRequest request,
        ServerCallContext context)
    {
        try
        {
            var appointmentsResponse = new AppointmentsResponse();
            var date = request.Date.ToDateTime();
            var appointments =
                await _appointmentRepository.GetByDateAndBarberAsync(date, request.BarberId);
            var data =
                appointments.Select(_mapper.Map<Customers_DAL.Entities.Appointment, AppointmentResponse>);
            appointmentsResponse.Data.AddRange(data);
            return appointmentsResponse;
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }

    public override async Task<AppointmentsResponse> GetBarbersAppointments(GetBarbersAppointmentsRequest request,
        ServerCallContext context)
    {
        try
        {
            var appointmentsResponse = new AppointmentsResponse();
            var appointments =
                await _appointmentRepository.GetBarbersAppointmentsAsync(request.BarberId);
            var data =
                appointments.Select(_mapper.Map<Customers_DAL.Entities.Appointment, AppointmentResponse>);
            appointmentsResponse.Data.AddRange(data);
            return appointmentsResponse;
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
}