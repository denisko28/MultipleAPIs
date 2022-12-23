using AutoMapper;
using Grpc.Core;
using HR_BLL.Protos;
using HR_DAL.Entities;
using HR_DAL.Exceptions;
using HR_DAL.Repositories.Abstract;

namespace HR_BLL.Grpc;

public class BarbersService : Barbers.BarbersBase
{
    private readonly IBarberRepository _barberRepository;
    private readonly IEmployeeDayOffRepository _employeeDayOffRepository;
    private readonly IMapper _mapper;

    public BarbersService(IBarberRepository barberRepository, IEmployeeDayOffRepository employeeDayOffRepository,
        IMapper mapper)
    {
        _barberRepository = barberRepository;
        _employeeDayOffRepository = employeeDayOffRepository;
        _mapper = mapper;
    }

    public override async Task<BarbersResponse> GetAll(GetAllBarbersRequest request, ServerCallContext context)
    {
        try
        {
            var barbersResponse = new BarbersResponse();
            var barbers = await _barberRepository.GetAllAsync();
            var data = barbers.Select(_mapper.Map<Barber, BarberResponse>);
            barbersResponse.Data.AddRange(data);
            return barbersResponse;
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }

    public override async Task<BarberResponse> GetById(GetBarberByIdRequest request, ServerCallContext context)
    {
        try
        {
            var barber = await _barberRepository.GetByIdAsync(request.Id);
            return _mapper.Map<Barber, BarberResponse>(barber);
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

    public override async Task<BarbersDayOffsResponse> GetBarbersDayOffs(GetBarberByIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var response = new BarbersDayOffsResponse();
            var dayOffs = await _employeeDayOffRepository.GetDayOffsByEmployee(request.Id);
            var dayOffsData = dayOffs.Select(_mapper.Map<DayOff, BarbersDayOffResponse>);
            response.Data.AddRange(dayOffsData);

            return response;
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
}