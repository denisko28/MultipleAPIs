using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Moq;

namespace Customers_BLL_Tests.MockRepositories;

public class MockAppointmentRepository: Mock<IAppointmentRepository>
{
    public MockAppointmentRepository MockGetByIdAsync(Appointment result)
    {
        Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentRepository MockGetAllAsync_AsAdmin(IEnumerable<Appointment> result)
    {
        Setup(x => x.GetAllAsync())
            .ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentRepository MockGetAllAsync_AsManager(IEnumerable<Appointment> result)
    {
        Setup(x => x.GetByBranchAsync(It.IsAny<int>()))
            .ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentRepository MockGetByDateAsync(IEnumerable<Appointment> result)
    {
        Setup(x => x.GetByDateAsync(It.IsAny<DateTime>())).ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentRepository MockGetAppointmentServicesAsync(IEnumerable<Service> result)
    {
        Setup(x => x.GetAppointmentServicesAsync(It.IsAny<int>()))
            .ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentRepository MockUpdateAsync()
    {
        Setup(x => x.UpdateAsync(It.IsAny<Appointment>()));
        return this;
    }
    
    public MockAppointmentRepository MockDeleteByIdAsync()
    {
        Setup(x => x.DeleteByIdAsync(It.IsAny<int>()));
        return this;
    }
}