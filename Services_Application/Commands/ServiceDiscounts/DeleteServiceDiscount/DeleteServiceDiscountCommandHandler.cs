using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.ServiceDiscounts.DeleteServiceDiscount
{
    public class DeleteServiceDiscountCommandHandler: IRequestHandler<DeleteServiceDiscountCommand>
    {
        private readonly SqlDbContext sqlDbContext;
        
        private readonly IMongoCollection<ServiceDiscount> collection;
        
        private readonly DbSet<ServiceDiscount> table;

        public DeleteServiceDiscountCommandHandler(MongoDbContext mongoDbContext, SqlDbContext sqlDbContext)
        {
            collection = mongoDbContext.Collection<ServiceDiscount>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<ServiceDiscount>();
        }

        public async Task<Unit> Handle(DeleteServiceDiscountCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<ServiceDiscount>.Filter.Eq(c => c.Id, request.Id);
            var entity = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if (entity == null)
                throw new EntityNotFoundException(nameof(ServiceDiscount), request.Id);

            await Task.Run(() => table.Remove(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.DeleteOneAsync(filter, cancellationToken);
            
            return Unit.Value;
        }
    }
}