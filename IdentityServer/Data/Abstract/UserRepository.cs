using IdentityServer.Models;

namespace IdentityServer.Data.Abstract;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserById(int id);

    Task<IEnumerable<ApplicationUser>> GetUsersByIds(IEnumerable<int> ids);
}