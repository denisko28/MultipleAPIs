using System.Security.Claims;
using Customers_API_Tests.MockServices;
using Customers_API.Controllers;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using FluentAssertions;
using IdentityServer.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Customers_API_Tests;

public class CustomerControllerTest
{
    private static HttpContext MakeFakeContext()
    {
        var context = new Mock<HttpContext>();
        var user = new Mock<ClaimsPrincipal>();
        
        var claims = new List<Claim>
        {
            new (CustomJwtClaimTypes.UserId, "1"),
            new (CustomJwtClaimTypes.Email, "petro123@gmail.com"),
            new (CustomJwtClaimTypes.FirstName, "Petro"),
            new (CustomJwtClaimTypes.LastName, "Sagajdachnyj"),
            new (CustomJwtClaimTypes.BranchId, ""),
            new (CustomJwtClaimTypes.Role, UserRoles.Admin)
        };

        context.Setup(c=> c.User).Returns(user.Object);
        user.Setup(c=> c.Identity).Returns(new ClaimsIdentity(claims));

        return context.Object;
    }


    [Fact]
    public async Task Get()
    {
        // Arrange
        var serviceResult = new List<CustomerResponse>{
            new()
            {
                UserId = 16,
                VisitsNum = 0 
            },
            new()
            {
                UserId = 17,
                VisitsNum = 0 
            },
            new()
            {
                UserId = 18,
                VisitsNum = 0 
            },
            new()
            {
                UserId = 19,
                VisitsNum = 0 
            },
        };
        var mockCustomerService = new MockCustomerService().MockGetAllAsync(serviceResult);
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };

        // Act
        var result = await controller.Get();
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Get_By_Id_EntityNotFound_False()
    {
        // Arrange
        const int testId = 1;
        var serviceResult = new CustomerResponse
        {
            UserId = 17,
            VisitsNum = 0
        };
        var mockCustomerService = new MockCustomerService().MockGetByIdAsync_EntityNotFound_False(serviceResult);
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.Get(testId);
        var objectResult = result.Result as ObjectResult;
    
        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task Get_By_Id_EntityNotFound_True()
    {
        // Arrange
        const int testId = 1;
        var mockCustomerService = new MockCustomerService().MockGetByIdAsync_EntityNotFound_True();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.Get(testId);
        var objectResult = result.Result as ObjectResult;
    
        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task GetCustomersAppointments_Accessible_True_EntityNotFound_False()
    {
        // Arrange
        const int testCustomerId = 5;
        var serviceResult = new List<CustomersAppointmentResponse>
        {
            new()
            {
                Id = 1,
                BarberUserId = 4,
                AppointmentStatusId = 3,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-02-27"),
                BeginTime = TimeSpan.Parse("12:00:00"),
                EndTime = TimeSpan.Parse("13:00:00"),
                BranchAddress = "вул. Героїв майдану 55, Чернівці, Чернівецька область"
            },
            new()
            {
                Id = 3,
                BarberUserId = 3,
                AppointmentStatusId = 2,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-02-27"),
                BeginTime = TimeSpan.Parse("17:00:00"),
                EndTime = TimeSpan.Parse("18:30:00"),
                BranchAddress = "вул. Героїв майдану 55, Чернівці, Чернівецька область"
            }  
        };
        var mockCustomerService = 
            new MockCustomerService().MockGetCustomersAppointments_Accessible_True_EntityNotFound_False(serviceResult);
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.GetCustomersAppointments(testCustomerId);
        var objectResult = result.Result as ObjectResult;
    
        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task GetCustomersAppointments_Accessible_False()
    {
        // Arrange
        const int testCustomerId = 5;
        var mockCustomerService = new MockCustomerService().MockGetCustomersAppointments_Accessible_False();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.GetCustomersAppointments(testCustomerId);
        var objectResult = result.Result as ObjectResult;
    
        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }
    
    [Fact]
    public async Task GetCustomersAppointments_Accessible_True_EntityNotFound_True()
    {
        // Arrange
        const int testCustomerId = 5;
        var mockCustomerService = 
            new MockCustomerService().MockGetCustomersAppointments_Accessible_True_EntityNotFound_True();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.GetCustomersAppointments(testCustomerId);
        var objectResult = result.Result as ObjectResult;
    
        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task Put_Accessible_True_EntityNotFound_False()
    {
        // Arrange
        var testCustomer = new CustomerRequest
        {
            UserId = 16,
            VisitsNum = 5
        };
        var mockCustomerService = new MockCustomerService().MockUpdateAsync_Accessible_True_EntityNotFound_False();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.Put(testCustomer);
    
        // Assert
        result.GetType().Should().Be(typeof(OkResult));
    }
    
    [Fact]
    public async Task Put_Accessible_False()
    {
        // Arrange
        var testCustomer = new CustomerRequest
        {
            UserId = 16,
            VisitsNum = 5
        };
        var mockCustomerService = new MockCustomerService().MockUpdateAsync_Accessible_False();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.Put(testCustomer) as ObjectResult;
    
        // Assert
        result!.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }
    
    [Fact]
    public async Task Put_Accessible_True_EntityNotFound_True()
    {
        // Arrange
        var testCustomer = new CustomerRequest
        {
            UserId = 16,
            VisitsNum = 5
        };
        var mockCustomerService = new MockCustomerService().MockUpdateAsync_Accessible_True_EntityNotFound_True();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.Put(testCustomer) as ObjectResult;
    
        // Assert
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task Delete_EntityNotFound_False()
    {
        // Arrange
        const int testId = 5;
        var mockCustomerService = new MockCustomerService().MockDeleteByIdAsync_EntityNotFound_False();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.Delete(testId);
    
        // Assert
        result.GetType().Should().Be(typeof(OkResult));
    }
    
    [Fact]
    public async Task Delete_EntityNotFound_True()
    {
        // Arrange
        const int testId = 5;
        var mockCustomerService = new MockCustomerService().MockDeleteByIdAsync_EntityNotFound_True();
        var controller = new CustomerController(mockCustomerService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };
    
        // Act
        var result = await controller.Delete(testId) as ObjectResult;
    
        // Assert
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}