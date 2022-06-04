using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
    }
}
