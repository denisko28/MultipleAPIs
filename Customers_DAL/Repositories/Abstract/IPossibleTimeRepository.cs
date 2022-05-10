using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;

namespace Customers_DAL.Repositories.Abstract
{
  public interface IPossibleTimeRepository
  {
    Task<IEnumerable<PossibleTime>> GetAllAsync();

    Task<IEnumerable<PossibleTime>> GetAllAvailableAsync();

    Task UpdateAsync(PossibleTime entity);
  }
}
