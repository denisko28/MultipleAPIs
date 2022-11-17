using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.ServiceDiscounts.UpdateServiceDiscount
{
    public class UpdateServiceDiscountCommandHandler : IRequestHandler<UpdateServiceDiscountCommand>
    {
        private readonly IMongoCollection<ServiceDiscount> collection;

        private readonly IMapper mapper;

        public UpdateServiceDiscountCommandHandler(MongoDbContext mongoDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<ServiceDiscount>();

            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateServiceDiscountCommand request, CancellationToken cancellationToken)
        {
            var filter = 
                Builders<ServiceDiscount>.Filter.Eq(c => c.Id, request.ServiceDiscountRequest.Id);
            var serviceDiscountExists = await collection.Find(filter).AnyAsync(cancellationToken);

            if (!serviceDiscountExists)
                throw new EntityNotFoundException(nameof(ServiceDiscount), request.ServiceDiscountRequest.Id);

            var entity = 
                mapper.Map<ServiceDiscountRequest, ServiceDiscount>(request.ServiceDiscountRequest);
            await collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);

            return Unit.Value;
        }
    }
}