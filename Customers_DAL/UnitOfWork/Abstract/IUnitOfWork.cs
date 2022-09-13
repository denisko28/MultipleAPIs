using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Customers_DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }
        
        IAppointmentServiceRepository AppointmentServiceRepository { get; set; }

        IPossibleTimeRepository PossibleTimeRepository { get; }
        
        IBarberRepository BarberRepository { get; }
        
        ICustomerRepository CustomerRepository { get; }
        
        IEmployeeRepository EmployeeRepository { get; }
        
        IServiceRepository ServiceRepository { get; }
        
        IUserRepository UserRepository { get; }
        
        UserManager<User> UserManager { get; }

        SignInManager<User> SignInManager { get; }

        Task SaveChangesAsync();
    }
}
