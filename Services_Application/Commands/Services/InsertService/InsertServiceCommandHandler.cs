using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Services.InsertService
{
    public class InsertServiceCommandHandler: IRequestHandler<InsertServiceCommand>
    {
        private readonly SqlDbContext sqlDbContext;
        
        private readonly IMongoCollection<Service> collection;
        
        private readonly DbSet<Service> table;
        
        private readonly IMapper mapper;

        public InsertServiceCommandHandler(MongoDbContext mongoDbContext, SqlDbContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Service>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Service>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(InsertServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ServiceRequest, Service>(request.ServiceRequest);
            entity.Id = 0;

            await table.AddAsync(entity, cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}