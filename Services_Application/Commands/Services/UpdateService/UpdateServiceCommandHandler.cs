using AutoMapper;
using Common.Events.ServiceEvents;
using MassTransit;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Services.UpdateService
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly IMongoCollection<Service> _collection;

        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IMapper _mapper;

        public UpdateServiceCommandHandler(MongoDbContext mongoDbContext, IPublishEndpoint publishEndpoint,
            IMapper mapper)
        {
            _collection = mongoDbContext.Collection<Service>();
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Service>.Filter.Eq(c => c.Id, request.ServiceRequest.Id);
            var serviceExists = await _collection.Find(filter).AnyAsync(cancellationToken);

            if (!serviceExists)
                throw new EntityNotFoundException(nameof(Service), request.ServiceRequest.Id);

            var entity = _mapper.Map<ServiceRequest, Service>(request.ServiceRequest);
            await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);

            var eventMessage = _mapper.Map<ServiceUpdatedEvent>(entity);
            await _publishEndpoint.Publish(eventMessage, cancellationToken);

            return Unit.Value;
        }
    }
}