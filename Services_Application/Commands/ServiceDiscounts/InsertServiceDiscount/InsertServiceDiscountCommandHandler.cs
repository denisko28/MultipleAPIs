using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.ServiceDiscounts.InsertServiceDiscount
{
    public class InsertServiceDiscountCommandHandler: IRequestHandler<InsertServiceDiscountCommand>
    {
        private readonly SqlDbContext sqlDbContext;
        
        private readonly IMongoCollection<ServiceDiscount> collection;
        
        private readonly DbSet<ServiceDiscount> table;
        
        private readonly IMapper mapper;

        public InsertServiceDiscountCommandHandler(MongoDbContext mongoDbContext, SqlDbContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<ServiceDiscount>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<ServiceDiscount>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(InsertServiceDiscountCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ServiceDiscountRequest, ServiceDiscount>(request.ServiceDiscountRequest);
            entity.Id = 0;

            await table.AddAsync(entity, cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}