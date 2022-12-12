using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchResponseDto>> GetAllAsync();

        Task<BranchResponseDto> GetByIdAsync(int id);

        Task<int> InsertAsync(BranchPostRequestDto requestDto);

        Task<bool> UpdateAsync(BranchRequestDto requestDto);

        Task DeleteByIdAsync(int id);
    }
}
