using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MultipleAPIs.HR_DAL.UnitOfWorks.Abstract;
using MultipleAPIs.HR_BLL.DTO.Requests;
using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;
using MultipleAPIs.HR_BLL.Services.Abstract;

namespace MultipleAPIs.HR_BLL.Services.Concrete
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IBranchRepository branchRepository;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            branchRepository = unitOfWork.BranchRepository;
        }

        public async Task<IEnumerable<BranchResponse>> GetAllAsync()
        {
            var results = await branchRepository.GetAllAsync();
            return results.Select(mapper.Map<Branch, BranchResponse>);
        }

        public async Task<BranchResponse> GetByIdAsync(int Id)
        {
            var result = await branchRepository.GetByIdAsync(Id);
            return mapper.Map<Branch, BranchResponse>(result);
        }

        public async Task<int> InsertAsync(BranchRequest request)
        {
            var entity = mapper.Map<BranchRequest, Branch>(request);
            var result = await branchRepository.InsertAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task<bool> UpdateAsync(BranchRequest request)
        {
            var entity = mapper.Map<BranchRequest, Branch>(request);
            var result = await branchRepository.UpdateAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task DeleteByIdAsync(int Id)
        {
            await branchRepository.DeleteByIdAsync(Id);
            unitOfWork.Commit();
        }
    }
}
