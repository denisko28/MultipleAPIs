using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Responses;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Queries.Services.GetAllServices
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceResponse>>
    {
        private readonly IMongoCollection<Service> collection;
        
        private readonly IMapper mapper;

        public GetAllServicesQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            collection = dbContext.Collection<Service>();
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ServiceResponse>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Service> results = await collection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
            return results.Select(mapper.Map<Service, ServiceResponse>);;
        }
    }
}