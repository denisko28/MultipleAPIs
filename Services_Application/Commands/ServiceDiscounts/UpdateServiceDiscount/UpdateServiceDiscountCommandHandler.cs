using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.ServiceDiscounts.UpdateServiceDiscount
{
    public class UpdateServiceDiscountCommandHandler: IRequestHandler<UpdateServiceDiscountCommand>
    {
        private readonly SqlDbContext sqlDbContext;
        
        private readonly IMongoCollection<ServiceDiscount> collection;
        
        private readonly DbSet<ServiceDiscount> table;
        
        private readonly IMapper mapper;

        public UpdateServiceDiscountCommandHandler(MongoDbContext mongoDbContext, SqlDbContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<ServiceDiscount>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<ServiceDiscount>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateServiceDiscountCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<ServiceDiscount>.Filter.Eq(c => c.Id, request.ServiceDiscountRequest.Id);
            var serviceDiscountExists = await collection.Find(filter).AnyAsync(cancellationToken);
            
            if (!serviceDiscountExists)
                throw new EntityNotFoundException(nameof(ServiceDiscount), request.ServiceDiscountRequest.Id);
            
            var entity = mapper.Map<ServiceDiscountRequest, ServiceDiscount>(request.ServiceDiscountRequest);

            await Task.Run(() => table.Update(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}