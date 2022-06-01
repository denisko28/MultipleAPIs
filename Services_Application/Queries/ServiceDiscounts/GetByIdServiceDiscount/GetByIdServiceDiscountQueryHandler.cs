using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Responses;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Queries.ServiceDiscounts.GetByIdServiceDiscount
{
    public class GetByIdServiceDiscountQueryHandler : IRequestHandler<GetByIdServiceDiscountQuery, ServiceDiscountResponse>
    {
        private readonly IMongoCollection<ServiceDiscount> serviceDiscCollection;
        
        private readonly IMongoCollection<Branch> branchCollection;
        
        private readonly IMapper mapper;

        public GetByIdServiceDiscountQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            serviceDiscCollection = dbContext.Collection<ServiceDiscount>();
            branchCollection = dbContext.Collection<Branch>();
            this.mapper = mapper;
        }

        public async Task<ServiceDiscountResponse> Handle(GetByIdServiceDiscountQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<ServiceDiscount>.Filter.Eq(c => c.Id, request.Id);
            var serviceDiscount = await serviceDiscCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if(serviceDiscount == null)
                throw new EntityNotFoundException(nameof(Service), request.Id);
            
            var response = mapper.Map<ServiceDiscount, ServiceDiscountResponse>(serviceDiscount);
            var branchFilter = Builders<Branch>.Filter.Eq(branch => branch.Id, serviceDiscount.BranchId);
            var branch = await branchCollection.Find(branchFilter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                
            if(branch == null)
                throw new EntityNotFoundException(nameof(Branch), serviceDiscount.BranchId);
                
            response.BranchDescript = branch.Descript;
            
            return response;
        }
    }
}