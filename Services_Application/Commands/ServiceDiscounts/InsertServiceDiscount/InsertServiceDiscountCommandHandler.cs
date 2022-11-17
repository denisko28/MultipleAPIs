using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.ServiceDiscounts.InsertServiceDiscount
{
    public class InsertServiceDiscountCommandHandler: IRequestHandler<InsertServiceDiscountCommand>
    {
        private readonly IMongoCollection<ServiceDiscount> _collection;
        
        private readonly IMongoCollection<Counters> _countersCollection;
        
        private readonly IMapper _mapper;

        public InsertServiceDiscountCommandHandler(MongoDbContext mongoDbContext, IMapper mapper)
        {
            _collection = mongoDbContext.Collection<ServiceDiscount>();
            _countersCollection = mongoDbContext.Collection<Counters>();
            _mapper = mapper;
        }

        public async Task<Unit> Handle(InsertServiceDiscountCommand request, CancellationToken cancellationToken)
        {
            var entity = 
                _mapper.Map<ServiceDiscountPostRequest, ServiceDiscount>(request.ServiceDiscountPostRequest);
            var filter = Builders<Counters>.Filter.Eq(a => a.Id, "serviceDiscountId");
            var update = Builders<Counters>.Update.Inc(a => a.SequenceValue, 1);
            var sequence = _countersCollection.FindOneAndUpdate(filter, update, cancellationToken: cancellationToken);
            entity.Id = sequence.SequenceValue + 1;

            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}