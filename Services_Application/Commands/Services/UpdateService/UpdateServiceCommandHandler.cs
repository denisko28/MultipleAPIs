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

namespace Services_Application.Commands.Services.UpdateService
{
    public class UpdateServiceCommandHandler: IRequestHandler<UpdateServiceCommand>
    {
        private readonly SqlDbContext sqlDbContext;
        
        private readonly IMongoCollection<Service> collection;
        
        private readonly DbSet<Service> table;
        
        private readonly IMapper mapper;

        public UpdateServiceCommandHandler(MongoDbContext mongoDbContext, SqlDbContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Service>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Service>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Service>.Filter.Eq(c => c.Id, request.ServiceRequest.Id);
            var serviceExists = await collection.Find(filter).AnyAsync(cancellationToken);
            
            if (!serviceExists)
                throw new EntityNotFoundException(nameof(Service), request.ServiceRequest.Id);
            
            var entity = mapper.Map<ServiceRequest, Service>(request.ServiceRequest);

            await Task.Run(() => table.Update(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}