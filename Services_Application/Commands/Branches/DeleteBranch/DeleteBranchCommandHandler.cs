using MediatR;
using MongoDB.Driver;
using Services_Application.Exceptions;
using Services_Domain.Entities;
using Services_Infrastructure;

namespace Services_Application.Commands.Branches.DeleteBranch
{
    public class DeleteBranchCommandHandler: IRequestHandler<DeleteBranchCommand>
    {
        private readonly IMongoCollection<Branch> collection;

        public DeleteBranchCommandHandler(MongoDbContext mongoDbContext)
        {
            collection = mongoDbContext.Collection<Branch>();
        }

        public async Task<Unit> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Branch>.Filter.Eq(c => c.Id, request.Id);
            var entity = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if (entity == null)
                throw new EntityNotFoundException(nameof(Branch), request.Id);

            await collection.DeleteOneAsync(filter, cancellationToken);
            
            return Unit.Value;
        }
    }
}