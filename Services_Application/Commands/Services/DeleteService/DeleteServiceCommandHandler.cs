using Common.Events.ServiceEvents;
using MassTransit;
using MediatR;
using MongoDB.Driver;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Services.DeleteService
{
    public class DeleteServiceCommandHandler: IRequestHandler<DeleteServiceCommand>
    {
        private readonly IMongoCollection<Service> _collection;

        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteServiceCommandHandler(MongoDbContext mongoDbContext, IPublishEndpoint publishEndpoint)
        {
            _collection = mongoDbContext.Collection<Service>();
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Service>.Filter.Eq(c => c.Id, request.Id);
            var entity = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if (entity == null)
                throw new EntityNotFoundException(nameof(Service), request.Id);

            await _collection.DeleteOneAsync(filter, cancellationToken);
            
            await _publishEndpoint.Publish(new ServiceDeletedEvent{ Id = request.Id }, cancellationToken);
            
            return Unit.Value;
        }
    }
}