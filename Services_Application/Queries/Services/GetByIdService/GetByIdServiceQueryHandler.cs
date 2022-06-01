using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Responses;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Queries.Services.GetByIdService
{
    public class GetByIdServiceQueryHandler : IRequestHandler<GetByIdServiceQuery, ServiceResponse>
    {
        private readonly IMongoCollection<Service> collection;
        
        private readonly IMapper mapper;

        public GetByIdServiceQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            collection = dbContext.Collection<Service>();
            this.mapper = mapper;
        }

        public async Task<ServiceResponse> Handle(GetByIdServiceQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Service>.Filter.Eq(c => c.Id, request.Id);
            var result = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if(result == null)
                throw new EntityNotFoundException(nameof(Service), request.Id);
            
            return mapper.Map<Service, ServiceResponse>(result);
        }
    }
}