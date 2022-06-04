using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;

namespace Customers_BLL.Services.Abstract
{
    public interface IUserService
    {
      Task<IEnumerable<UserResponse>> GetAllAsync();

      Task<UserResponse> GetByIdAsync(int id);

      Task SetAvatarForUserAsync(ImageUploadRequest request);
    }
}
