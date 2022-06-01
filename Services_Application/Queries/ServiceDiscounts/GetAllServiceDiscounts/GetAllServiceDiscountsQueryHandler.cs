using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Responses;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Queries.ServiceDiscounts.GetAllServiceDiscounts
{
    public class GetAllServiceDiscountsQueryHandler : IRequestHandler<GetAllServiceDiscountsQuery, IEnumerable<ServiceDiscountResponse>>
    {
        private readonly IMongoCollection<ServiceDiscount> serviceDiscCollection;
        
        private readonly IMongoCollection<Branch> branchCollection;
        
        private readonly IMapper mapper;

        public GetAllServiceDiscountsQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            serviceDiscCollection = dbContext.Collection<ServiceDiscount>();
            branchCollection = dbContext.Collection<Branch>();
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDiscountResponse>> Handle(GetAllServiceDiscountsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ServiceDiscount> serviceDiscounts = 
                await serviceDiscCollection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
            var responses = new List<ServiceDiscountResponse>();
            foreach (var serviceDiscount in serviceDiscounts)
            {
                var response = mapper.Map<ServiceDiscount, ServiceDiscountResponse>(serviceDiscount);
                var filter = Builders<Branch>.Filter.Eq(branch => branch.Id, serviceDiscount.BranchId);
                var branch = await branchCollection.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                
                if(branch == null)
                    throw new EntityNotFoundException(nameof(Branch), serviceDiscount.BranchId);
                
                response.BranchDescript = branch.Descript;
                responses.Add(response);
            }
            return responses;
        }
    }
}