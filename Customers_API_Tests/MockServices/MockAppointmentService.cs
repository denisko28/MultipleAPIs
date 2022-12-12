using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Services.Abstract;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using IdentityServer.Helpers;
using Moq;

namespace Customers_API_Tests.MockServices;

public class MockAppointmentService: Mock<IAppointmentService>
{
    public MockAppointmentService MockGetByIdAsync_Accessible_True_EntityNotFound_False(AppointmentResponse result)
    {
        Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<UserClaimsModel>()))
            .ReturnsAsync(result);
        return this;
    }

    public MockAppointmentService MockGetByIdAsync_Accessible_False()
    {
        Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new ForbiddenAccessException("User does not have access!"));
        return this;
    }
    
    public MockAppointmentService MockGetByIdAsync_Accessible_True_EntityNotFound_True()
    {
        Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Appointment), 1));
        return this;
    }
    
    public MockAppointmentService MockGetAllAsync_Accessible_True(IEnumerable<AppointmentResponse> result)
    {
        Setup(x => x.GetAllAsync(It.IsAny<UserClaimsModel>())).ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentService MockGetAllAsync_Accessible_False()
    {
        Setup(x => x.GetAllAsync(It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new ForbiddenAccessException("User does not have access!"));
        return this;
    }
    
    public MockAppointmentService MockGetByDateAsync(IEnumerable<AppointmentResponse> result)
    {
        Setup(x => x.GetByDateAsync(It.IsAny<string>())).ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentService MockGetAppointmentServicesAsync_EntityNotFound_False(IEnumerable<ServiceResponse> result)
    {
        Setup(x => x.GetAppointmentServicesAsync(It.IsAny<int>()))
            .ReturnsAsync(result);
        return this;
    }
    
    public MockAppointmentService MockGetAppointmentServicesAsync_EntityNotFound_True()
    {
        Setup(x => x.GetAppointmentServicesAsync(It.IsAny<int>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Appointment), 5));
        return this;
    }
    
    public MockAppointmentService MockUpdateAsync_Accessible_True_EntityNotFound_False()
    {
        Setup(x => 
            x.UpdateAsync(It.IsAny<AppointmentRequest>(), It.IsAny<UserClaimsModel>()));
        return this;
    }
    
    public MockAppointmentService MockUpdateAsync_Accessible_False()
    {
        Setup(x => 
            x.UpdateAsync(It.IsAny<AppointmentRequest>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new ForbiddenAccessException("User does not have access!"));
        return this;
    }
    
    public MockAppointmentService MockUpdateAsync_Accessible_True_EntityNotFound_True()
    {
        Setup(x => 
            x.UpdateAsync(It.IsAny<AppointmentRequest>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Appointment), 5));
        return this;
    }
    
    public MockAppointmentService MockDeleteByIdAsync_EntityNotFound_False()
    {
        Setup(x => x.DeleteByIdAsync(It.IsAny<int>()));
        return this;
    }
    
    public MockAppointmentService MockDeleteByIdAsync_EntityNotFound_True()
    {
        Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Appointment), 5));
        return this;
    }
}