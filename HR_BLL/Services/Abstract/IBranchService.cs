using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_BLL.DTO.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_BLL.Services.Abstract
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchResponse>> GetAllAsync();

        Task<BranchResponse> GetByIdAsync(int Id);

        Task<int> InsertAsync(BranchRequest request);

        Task<bool> UpdateAsync(BranchRequest request);

        Task DeleteByIdAsync(int Id);
    }
}
