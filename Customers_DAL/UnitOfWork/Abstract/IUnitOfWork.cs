using System.Threading.Tasks;
using Customers_DAL.Repositories.Abstract;

namespace Customers_DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }
        
        IAppointmentServiceRepository AppointmentServiceRepository { get; set; }

        IPossibleTimeRepository PossibleTimeRepository { get; }
        
        IBarberRepository BarberRepository { get; }
        
        ICustomerRepository CustomerRepository { get; }

        Task SaveChangesAsync();
    }
}
