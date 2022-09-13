using System.Threading.Tasks;
using Customers_DAL.Entities;

namespace Customers_DAL.Repositories.Abstract
{
    public interface IServiceRepository
    {
        Task<Service> GetById(int id);
    }
}