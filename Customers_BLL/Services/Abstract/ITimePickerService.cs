using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Responses;

namespace Customers_BLL.Services.Abstract
{
    public interface ITimePickerService
    {
        Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr);
    }
}