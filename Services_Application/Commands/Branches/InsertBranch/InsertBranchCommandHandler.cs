using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Services_Application.DTO.Requests;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Branches.InsertBranch
{
    public class InsertBranchCommandHandler: IRequestHandler<InsertBranchCommand>
    {
        private readonly IMongoCollection<Branch> collection;
        
        private readonly IMapper mapper;

        public InsertBranchCommandHandler(MongoDbContext mongoDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Branch>();
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(InsertBranchCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<BranchRequest, Branch>(request.BranchRequest);
            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}