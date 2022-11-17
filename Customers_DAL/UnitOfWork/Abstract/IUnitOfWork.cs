using Customers_DAL.Repositories.Abstract;

namespace Customers_DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }
        
        IAppointmentServiceRepository AppointmentServiceRepository { get; set; }

        IPossibleTimeRepository PossibleTimeRepository { get; }
        
        ICustomerRepository CustomerRepository { get; }
        
        IBranchRepository BranchRepository { get; }
        
        IServiceRepository ServiceRepository { get; }

        Task SaveChangesAsync();
    }
}
