using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Helpers;

namespace Customers_BLL.Services.Abstract
{
    public interface IUserService
    {
      Task<IEnumerable<UserResponse>> GetAllAsync();

      Task<UserResponse> GetByIdAsync(int id, UserClaimsModel userClaims);

      Task SetAvatarForUserAsync(ImageUploadRequest request, UserClaimsModel userClaims);

      Task DeleteAsync(int id);
    }
}
