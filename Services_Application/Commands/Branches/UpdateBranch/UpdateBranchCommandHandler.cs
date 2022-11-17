using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Branches.UpdateBranch
{
    public class UpdateBranchCommandHandler: IRequestHandler<UpdateBranchCommand>
    {
        private readonly IMongoCollection<Branch> collection;

        private readonly IMapper mapper;

        public UpdateBranchCommandHandler(MongoDbContext mongoDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Branch>();
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Branch>.Filter.Eq(c => c.Id, request.BranchRequest.Id);
            var serviceExists = await collection.Find(filter).AnyAsync(cancellationToken);
            
            if (!serviceExists)
                throw new EntityNotFoundException(nameof(Branch), request.BranchRequest.Id);
            
            var entity = mapper.Map<BranchRequest, Branch>(request.BranchRequest);
            await collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}