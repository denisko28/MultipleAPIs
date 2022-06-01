using System;
using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.Repositories.Abstract
{
    public interface ICustomerRepository : IDisposable
    {
        Task<Customer> GetByIdAsync(int id);
    }
}
