using AutoMapper;
using Common.Events.ServiceEvents;
using MassTransit;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Services.InsertService
{
    public class InsertServiceCommandHandler : IRequestHandler<InsertServiceCommand>
    {
        private readonly IMongoCollection<Service> _collection;

        private readonly IMongoCollection<Counters> _countersCollection;
        
        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IMapper _mapper;

        public InsertServiceCommandHandler(MongoDbContext mongoDbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _collection = mongoDbContext.Collection<Service>();
            _countersCollection = mongoDbContext.Collection<Counters>();
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(InsertServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ServicePostRequest, Service>(request.ServiceRequest);
            var filter = Builders<Counters>.Filter.Eq(a => a.Id, "serviceId");
            var update = Builders<Counters>.Update.Inc(a => a.SequenceValue, 1);
            var sequence = _countersCollection.FindOneAndUpdate(filter, update, cancellationToken: cancellationToken);
            entity.Id = sequence.SequenceValue + 1;

            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            
            var eventMessage = _mapper.Map<ServiceInsertedEvent>(entity);
            await _publishEndpoint.Publish(eventMessage, cancellationToken);
            
            return Unit.Value;
        }
    }
}