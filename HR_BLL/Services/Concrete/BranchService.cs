using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Services.Abstract;
using HR_DAL.Entities;
using HR_DAL.MongoRepositories.Abstract;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;

namespace HR_BLL.Services.Concrete
{
    public class BranchService : IBranchService
    {
        private readonly IMapper mapper;

        private readonly IBranchRepository branchRepository;

        private readonly IBranchMongoRepository branchMongoRepository;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.mapper = mapper;
            branchRepository = unitOfWork.BranchRepository;
            branchMongoRepository = unitOfWork.BranchMongoRepository;
        }

        public async Task<IEnumerable<BranchResponse>> GetAllAsync()
        {
            var results = await branchRepository.GetAllAsync();
            return results.Select(mapper.Map<Branch, BranchResponse>);
        }

        public async Task<BranchResponse> GetByIdAsync(int id)
        {
            var result = await branchRepository.GetByIdAsync(id);
            return mapper.Map<Branch, BranchResponse>(result);
        }

        public async Task<int> InsertAsync(BranchRequest request)
        {
            var entity = mapper.Map<BranchRequest, Branch>(request);
            var insertedId = await branchRepository.InsertAsync(entity);
            
            entity.Id = insertedId;
            await branchMongoRepository.InsertAsync(entity);
            
            return insertedId;
        }

        public async Task<bool> UpdateAsync(BranchRequest request)
        {
            var entity = mapper.Map<BranchRequest, Branch>(request);
            var result = await branchRepository.UpdateAsync(entity);
            
            await branchMongoRepository.UpdateAsync(entity);
            
            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await branchRepository.DeleteByIdAsync(id);
            
            await branchMongoRepository.DeleteByIdAsync(id);
        }
    }
}
