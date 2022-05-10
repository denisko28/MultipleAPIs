using System.Threading.Tasks;
using Customers_DAL.Repositories.Abstract;

namespace Customers_DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }

        IPossibleTimeRepository PossibleTimeRepository { get; }
        
        IBarberRepository BarberRepository { get; }

        Task SaveChangesAsync();
    }
}
