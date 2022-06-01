using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Services.DeleteService
{
    public class DeleteServiceCommandHandler: IRequestHandler<DeleteServiceCommand>
    {
        private readonly SqlDbContext sqlDbContext;
        
        private readonly IMongoCollection<Service> collection;
        
        private readonly DbSet<Service> table;

        public DeleteServiceCommandHandler(MongoDbContext mongoDbContext, SqlDbContext sqlDbContext)
        {
            collection = mongoDbContext.Collection<Service>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Service>();
        }

        public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Service>.Filter.Eq(c => c.Id, request.Id);
            var entity = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if (entity == null)
                throw new EntityNotFoundException(nameof(Service), request.Id);

            await Task.Run(() => table.Remove(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.DeleteOneAsync(filter, cancellationToken);
            
            return Unit.Value;
        }
    }
}