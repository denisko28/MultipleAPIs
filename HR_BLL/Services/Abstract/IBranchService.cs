using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchResponse>> GetAllAsync();

        Task<BranchResponse> GetByIdAsync(int id);

        Task<int> InsertAsync(BranchPostRequest request);

        Task<bool> UpdateAsync(BranchRequest request);

        Task DeleteByIdAsync(int id);
    }
}
