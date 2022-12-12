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

public class AppointmentControllerTest
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
    public async Task Get_Accessible_True()
    {
        // Arrange
        var serviceResult = new List<AppointmentResponse>{
            new()
            {
                Id = 1,
                BarberUserId = 4,
                CustomerUserId = 19,
                AppointmentStatusId = 3,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-02-27"),
                BeginTime = TimeSpan.Parse("12:00:00"),
                EndTime = TimeSpan.Parse("13:00:00")
            },
            new()
            {
                Id = 2,
                BarberUserId = 9,
                CustomerUserId = 16,
                AppointmentStatusId = 2,
                BranchId = 2,
                AppDate = DateTime.Parse("2022-03-01"),
                BeginTime = TimeSpan.Parse("16:00:00"),
                EndTime = TimeSpan.Parse("17:30:00")
            },
            new()
            {
                Id = 3,
                BarberUserId = 3,
                CustomerUserId = 18,
                AppointmentStatusId = 2,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-03-01"),
                BeginTime = TimeSpan.Parse("17:00:00"),
                EndTime = TimeSpan.Parse("18:30:00")
            }
        };
        var mockAppointmentService = new MockAppointmentService().MockGetAllAsync_Accessible_True(serviceResult);
        var controller = new AppointmentController(mockAppointmentService.Object)
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
    public async Task Get_Accessible_False()
    {
        // Arrange
        var mockAppointmentService = new MockAppointmentService().MockGetAllAsync_Accessible_False();
        var controller = new AppointmentController(mockAppointmentService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };

        // Act
        var result = await controller.Get();
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }
    
    [Fact]
    public async Task Get_By_Id_Accessible_True_EntityNotFound_False()
    {
        // Arrange
        const int testId = 1;
        var serviceResult = new AppointmentResponse
        {
            Id = 1,
            BarberUserId = 4,
            CustomerUserId = 19,
            AppointmentStatusId = 3,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-02-27"),
            BeginTime = TimeSpan.Parse("12:00:00"),
            EndTime = TimeSpan.Parse("13:00:00")
        };
        var mockAppointmentService = new MockAppointmentService().MockGetByIdAsync_Accessible_True_EntityNotFound_False(serviceResult);
        var controller = new AppointmentController(mockAppointmentService.Object)
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
    public async Task Get_By_Id_Accessible_False()
    {
        // Arrange
        const int testId = 1;
        var mockAppointmentService = new MockAppointmentService().MockGetByIdAsync_Accessible_False();
        var controller = new AppointmentController(mockAppointmentService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };

        // Act
        var result = await controller.Get(testId);
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }
    
    [Fact]
    public async Task Get_By_Id_Accessible_True_EntityNotFound_True()
    {
        // Arrange
        const int testId = 1;
        var mockAppointmentService = new MockAppointmentService().MockGetByIdAsync_Accessible_True_EntityNotFound_True();
        var controller = new AppointmentController(mockAppointmentService.Object)
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
    public async Task GetByDate()
    {
        // Arrange
        const string testDate = "2022-02-27";
        var serviceResult = new List<AppointmentResponse>()
        {
            new()
            {
                Id = 1,
                BarberUserId = 4,
                CustomerUserId = 19,
                AppointmentStatusId = 3,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-02-27"),
                BeginTime = TimeSpan.Parse("12:00:00"),
                EndTime = TimeSpan.Parse("13:00:00")
            },
            new()
            {
                Id = 3,
                BarberUserId = 3,
                CustomerUserId = 18,
                AppointmentStatusId = 2,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-02-27"),
                BeginTime = TimeSpan.Parse("17:00:00"),
                EndTime = TimeSpan.Parse("18:30:00")
            }  
        };
        
        var mockAppointmentService = new MockAppointmentService().MockGetByDateAsync(serviceResult);
        var controller = new AppointmentController(mockAppointmentService.Object);

        // Act
        var result = await controller.GetByDate(testDate);
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task GetServices_EntityNotFound_False()
    {
        // Arrange
        const int testAppointmentId = 5;
        var serviceResult = new List<ServiceResponse>
        {
            new()
            {
                Id = 1,
                Name = "Стрижка",
                Duration = 60,
                Price = 300,
                Available = true
            },
            new()
            {
                Id = 2,
                Name = "Стрижка з бородою",
                Duration = 90,
                Price = 450,
                Available = true
            },
            new()
            {
                Id = 3,
                Name = "Голова - камуфляж сивини",
                Duration = 30,
                Price = 200,
                Available = true
            }
        };
        var mockAppointmentService = 
            new MockAppointmentService().MockGetAppointmentServicesAsync_EntityNotFound_False(serviceResult);
        var controller = new AppointmentController(mockAppointmentService.Object);

        // Act
        var result = await controller.GetServices(testAppointmentId);
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task GetServices_EntityNotFound_True()
    {
        // Arrange
        const int testAppointmentId = 5;
        var mockAppointmentService = new MockAppointmentService().MockGetAppointmentServicesAsync_EntityNotFound_True();
        var controller = new AppointmentController(mockAppointmentService.Object);

        // Act
        var result = await controller.GetServices(testAppointmentId);
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task Put_Accessible_True_EntityNotFound_False()
    {
        // Arrange
        var testAppointment = new AppointmentRequest
        {
            Id = 5,
            BarberUserId = 6,
            CustomerUserId = 17,
            AppointmentStatusId = 2,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-12"),
            BeginTime = TimeSpan.Parse("16:15:00"),
            EndTime = TimeSpan.Parse("17:15:00")
        };
        var mockAppointmentService = new MockAppointmentService().MockUpdateAsync_Accessible_True_EntityNotFound_False();
        var controller = new AppointmentController(mockAppointmentService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };

        // Act
        var result = await controller.Put(testAppointment);

        // Assert
        result.GetType().Should().Be(typeof(OkResult));
    }
    
    [Fact]
    public async Task Put_Accessible_False()
    {
        // Arrange
        var testAppointment = new AppointmentRequest
        {
            Id = 5,
            BarberUserId = 6,
            CustomerUserId = 17,
            AppointmentStatusId = 2,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-12"),
            BeginTime = TimeSpan.Parse("16:15:00"),
            EndTime = TimeSpan.Parse("17:15:00")
        };
        var mockAppointmentService = new MockAppointmentService().MockUpdateAsync_Accessible_False();
        var controller = new AppointmentController(mockAppointmentService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };

        // Act
        var result = await controller.Put(testAppointment) as ObjectResult;

        // Assert
        result!.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }
    
    [Fact]
    public async Task Put_Accessible_True_EntityNotFound_True()
    {
        // Arrange
        var testAppointment = new AppointmentRequest
        {
            Id = 5,
            BarberUserId = 6,
            CustomerUserId = 17,
            AppointmentStatusId = 2,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-12"),
            BeginTime = TimeSpan.Parse("16:15:00"),
            EndTime = TimeSpan.Parse("17:15:00")
        };
        var mockAppointmentService = new MockAppointmentService().MockUpdateAsync_Accessible_True_EntityNotFound_True();
        var controller = new AppointmentController(mockAppointmentService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };

        // Act
        var result = await controller.Put(testAppointment) as ObjectResult;

        // Assert
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task Delete_EntityNotFound_False()
    {
        // Arrange
        const int testId = 5;
        var mockAppointmentService = new MockAppointmentService().MockDeleteByIdAsync_EntityNotFound_False();
        var controller = new AppointmentController(mockAppointmentService.Object)
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
        var mockAppointmentService = new MockAppointmentService().MockDeleteByIdAsync_EntityNotFound_True();
        var controller = new AppointmentController(mockAppointmentService.Object)
        {
            ControllerContext = new ControllerContext {HttpContext = MakeFakeContext()}
        };

        // Act
        var result = await controller.Delete(testId) as ObjectResult;

        // Assert
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}