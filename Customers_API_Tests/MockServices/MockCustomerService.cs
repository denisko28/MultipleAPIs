using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Services.Abstract;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using IdentityServer.Helpers;
using Moq;

namespace Customers_API_Tests.MockServices;

public class MockCustomerService : Mock<ICustomerService>
{
    public MockCustomerService MockGetAllAsync(IEnumerable<CustomerResponse> result)
    {
        Setup(x => x.GetAllAsync()).ReturnsAsync(result);
        return this;
    }

    public MockCustomerService MockGetByIdAsync_EntityNotFound_False(CustomerResponse result)
    {
        Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(result);
        return this;
    }

    public MockCustomerService MockGetByIdAsync_EntityNotFound_True()
    {
        Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Customer), 1));
        return this;
    }

    public MockCustomerService MockGetCustomersAppointments_Accessible_True_EntityNotFound_False(
        IEnumerable<CustomersAppointmentResponse> result)
    {
        Setup(x => 
                x.GetCustomersAppointments(It.IsAny<int>(), It.IsAny<UserClaimsModel>()))
            .ReturnsAsync(result);
        return this;
    }

    public MockCustomerService MockGetCustomersAppointments_Accessible_False()
    {
        Setup(x => 
                x.GetCustomersAppointments(It.IsAny<int>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new ForbiddenAccessException("User does not have access!"));
        return this;
    }

    public MockCustomerService MockGetCustomersAppointments_Accessible_True_EntityNotFound_True()
    {
        Setup(x => 
                x.GetCustomersAppointments(It.IsAny<int>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Customer), 1));
        return this;
    }

    public MockCustomerService MockUpdateAsync_Accessible_True_EntityNotFound_False()
    {
        Setup(x =>
            x.UpdateAsync(It.IsAny<CustomerRequest>(), It.IsAny<UserClaimsModel>()));
        return this;
    }

    public MockCustomerService MockUpdateAsync_Accessible_False()
    {
        Setup(x =>
                x.UpdateAsync(It.IsAny<CustomerRequest>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new ForbiddenAccessException("User does not have access!"));
        return this;
    }

    public MockCustomerService MockUpdateAsync_Accessible_True_EntityNotFound_True()
    {
        Setup(x =>
                x.UpdateAsync(It.IsAny<CustomerRequest>(), It.IsAny<UserClaimsModel>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Customer), 5));
        return this;
    }

    public MockCustomerService MockDeleteByIdAsync_EntityNotFound_False()
    {
        Setup(x => x.DeleteByIdAsync(It.IsAny<int>()));
        return this;
    }

    public MockCustomerService MockDeleteByIdAsync_EntityNotFound_True()
    {
        Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Customer), 5));
        return this;
    }
}