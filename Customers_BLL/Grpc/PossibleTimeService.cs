using AutoMapper;
using Customers_BLL.Protos;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Grpc.Core;

namespace Customers_BLL.Grpc;

public class PossibleTimeService : PossibleTime.PossibleTimeBase
{
    private readonly IPossibleTimeRepository _possibleTimeRepository;
    private readonly IMapper _mapper;

    public PossibleTimeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _possibleTimeRepository = unitOfWork.PossibleTimeRepository;
        _mapper = mapper;
    }

    public override async Task<PossibleTimesResponse> GetAll(GetAllPossibleTimeRequest request,
        ServerCallContext context)
    {
        var possibleTimesResponse = new PossibleTimesResponse();
        var allAvailable = await _possibleTimeRepository.GetAllAvailableAsync();
        var data = 
            allAvailable.Select(_mapper.Map<Customers_DAL.Entities.PossibleTime, PossibleTimeResponse>);
        possibleTimesResponse.Data.AddRange(data);
        return possibleTimesResponse;
    }
}