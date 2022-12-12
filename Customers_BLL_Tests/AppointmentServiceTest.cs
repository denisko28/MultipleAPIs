using AutoMapper;
using Customers_BLL_Tests.MockRepositories;
using Customers_BLL.Configurations;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_DAL.Entities;
using Customers_DAL.UnitOfWork.Abstract;
using FluentAssertions;
using IdentityServer.Helpers;
using Moq;
using AppointmentService = Customers_BLL.Services.Concrete.AppointmentService;

namespace Customers_BLL_Tests;

public class AppointmentServiceTest
{
    private readonly IMapper _mapper;
    
    public AppointmentServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task GetByIdAsync_AsAdmin()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Admin};
        var repositoryResult = new Appointment
        {
            Id = 4,
            BarberUserId = 6,
            CustomerUserId = 22,
            AppointmentStatusId = 1,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-08"),
            BeginTime = TimeSpan.Parse("13:30:00"),
            EndTime = TimeSpan.Parse("15:15:00")
        };
        var expectedResult = new AppointmentResponse
        {
            Id = 4,
            BarberUserId = 6,
            CustomerUserId = 22,
            AppointmentStatusId = 1,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-08"),
            BeginTime = TimeSpan.Parse("13:30:00"),
            EndTime = TimeSpan.Parse("15:15:00")
        };
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetByIdAsync(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var result = await sut.GetByIdAsync(4, testClaimsModel);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task GetByIdAsync_AsManager_Accessible_True()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Manager, BranchId = 2};
        var repositoryResult = new Appointment
        {
            Id = 2,
            BarberUserId = 9,
            CustomerUserId = 16,
            AppointmentStatusId = 2,
            BranchId = 2,
            AppDate = DateTime.Parse("2022-03-01"),
            BeginTime = TimeSpan.Parse("16:00:00"),
            EndTime = TimeSpan.Parse("17:30:00")
        };
        var expectedResult = new AppointmentResponse
        {
            Id = 2,
            BarberUserId = 9,
            CustomerUserId = 16,
            AppointmentStatusId = 2,
            BranchId = 2,
            AppDate = DateTime.Parse("2022-03-01"),
            BeginTime = TimeSpan.Parse("16:00:00"),
            EndTime = TimeSpan.Parse("17:30:00")
        };
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetByIdAsync(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var result = await sut.GetByIdAsync(2, testClaimsModel);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetByIdAsync_AsManager_Accessible_False()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Manager, BranchId = 2};
        var repositoryResult = new Appointment
        {
            Id = 2,
            BarberUserId = 9,
            CustomerUserId = 16,
            AppointmentStatusId = 2,
            BranchId = 3,
            AppDate = DateTime.Parse("2022-03-01"),
            BeginTime = TimeSpan.Parse("16:00:00"),
            EndTime = TimeSpan.Parse("17:30:00")
        };
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetByIdAsync(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.GetByIdAsync(2, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeTrue();
    }

    [Fact]
    public async Task GetAllAsync_AsAdmin()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Admin};
        List<Appointment> repositoryResult = new()
        {
            new Appointment()
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
            new Appointment()
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
            new Appointment()
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
        var expectedResult = repositoryResult.Select(_mapper.Map<Appointment, AppointmentResponse>);
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetAllAsync_AsAdmin(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var result = await sut.GetAllAsync(testClaimsModel);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task GetAllAsync_AsManager()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Manager, BranchId = 1};
        List<Appointment> repositoryResult = new()
        {
            new Appointment
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
            new Appointment
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
        List<AppointmentResponse> expectedResult = new()
        {
            new AppointmentResponse
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
            new AppointmentResponse
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
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetAllAsync_AsManager(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var result = await sut.GetAllAsync(testClaimsModel);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task GetByDateAsync_ExistingDate_True()
    {
        // Arrange
        const string testDate = "2022-02-27";
        List<Appointment> repositoryResult = new()
        {
            new Appointment
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
            new Appointment
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
        List<AppointmentResponse> expectedResult = new()
        {
            new AppointmentResponse
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
            new AppointmentResponse
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
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetByDateAsync(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var result = await sut.GetByDateAsync(testDate);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task GetByDateAsync_ExistingDate_False()
    {
        // Arrange
        const string testDate = "2022-02-27";
        List<Appointment> repositoryResult = new();
        List<AppointmentResponse> expectedResult = new();
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetByDateAsync(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var result = await sut.GetByDateAsync(testDate);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetAppointmentServicesAsync()
    {
        // Arrange
        const int appointmentsId = 5;
        List<Service> repositoryResult = new()
        {
            new Service
            {
                Id = 1,
                Name = "Стрижка",
                Duration = 60,
                Price = 300,
                Available = true
            },
            new Service
            {
                Id = 2,
                Name = "Стрижка з бородою",
                Duration = 90,
                Price = 450,
                Available = true
            },
            new Service
            {
                Id = 3,
                Name = "Голова - камуфляж сивини",
                Duration = 30,
                Price = 200,
                Available = true
            }
        };
        List<ServiceResponse> expectedResult = new()
        {
            new ServiceResponse
            {
                Id = 1,
                Name = "Стрижка",
                Duration = 60,
                Price = 300,
                Available = true
            },
            new ServiceResponse
            {
                Id = 2,
                Name = "Стрижка з бородою",
                Duration = 90,
                Price = 450,
                Available = true
            },
            new ServiceResponse
            {
                Id = 3,
                Name = "Голова - камуфляж сивини",
                Duration = 30,
                Price = 200,
                Available = true
            }
        };
        var mockAppointmentRepository = new MockAppointmentRepository().MockGetAppointmentServicesAsync(repositoryResult);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);

        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var result = await sut.GetAppointmentServicesAsync(appointmentsId);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdateAsync_AsAdmin()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Admin};
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
        var mockAppointmentRepository = new MockAppointmentRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);
        
        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testAppointment, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_AsManager_Accessible_True()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Manager, BranchId = 1};
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
        var mockAppointmentRepository = new MockAppointmentRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);
        
        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testAppointment, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_AsManager_Accessible_False()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Manager, BranchId = 3};
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
        var mockAppointmentRepository = new MockAppointmentRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);
        
        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testAppointment, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeTrue();
    }
    
    [Fact]
    public async Task UpdateAsync_AsBarber_Accessible_True()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Barber, UserId = 6};
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
        var mockAppointmentRepository = new MockAppointmentRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);
        
        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testAppointment, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_AsBarber_Accessible_False()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Barber, UserId = 9};
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
        var mockAppointmentRepository = new MockAppointmentRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);
        
        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testAppointment, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeTrue();
    }
    
    [Fact]
    public async Task DeleteByIdAsync()
    {
        // Arrange
        const int testAppointmentId = 4;
        var mockAppointmentRepository = new MockAppointmentRepository().MockDeleteByIdAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.AppointmentRepository)
            .Returns(mockAppointmentRepository.Object);
        
        // Act
        var sut = new AppointmentService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.DeleteByIdAsync(testAppointmentId);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
}