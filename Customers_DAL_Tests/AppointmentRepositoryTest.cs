using Customers_DAL;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Concrete;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Customers_DAL_Tests;

public class AppointmentRepositoryTest
{
    private static readonly List<Appointment> AppointmentsList = new()
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
        },
        new Appointment()
        {
            Id = 4,
            BarberUserId = 6,
            CustomerUserId = 22,
            AppointmentStatusId = 1,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-08"),
            BeginTime = TimeSpan.Parse("13:30:00"),
            EndTime = TimeSpan.Parse("15:15:00")
        },
        new Appointment()
        {
            Id = 5,
            BarberUserId = 8,
            CustomerUserId = 17,
            AppointmentStatusId = 2,
            BranchId = 2,
            AppDate = DateTime.Parse("2022-03-12"),
            BeginTime = TimeSpan.Parse("16:15:00"),
            EndTime = TimeSpan.Parse("17:15:00")
        },
        new Appointment()
        {
            Id = 6,
            BarberUserId = 5,
            CustomerUserId = 21,
            AppointmentStatusId = 1,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-10"),
            BeginTime = TimeSpan.Parse("10:00:00"),
            EndTime = TimeSpan.Parse("10:15:00")
        },
        new Appointment()
        {
            Id = 7,
            BarberUserId = 6,
            CustomerUserId = 23,
            AppointmentStatusId = 2,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-15"),
            BeginTime = TimeSpan.Parse("15:00:00"),
            EndTime = TimeSpan.Parse("15:45:00")
        },
        new Appointment()
        {
            Id = 8,
            BarberUserId = 6,
            CustomerUserId = 18,
            AppointmentStatusId = 2,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-15"),
            BeginTime = TimeSpan.Parse("17:00:00"),
            EndTime = TimeSpan.Parse("18:00:00")
        }
    };

    private static readonly List<Appointment> EmptyAppointmentsList = new();
    
    [Fact]
    public async Task GetByIdAsync_ExistingId_True()
    {
        // Arrange
        var expectedResult = AppointmentsList[4];
        const int testId = 5;

        var dbSetMock = AppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        var result = await sut.GetByIdAsync(testId);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task GetByIdAsync_ExistingId_False()
    {
        // Arrange
        const int testId = 18;

        var dbSetMock = AppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        var exceptionThrown = false;
        try
        {
            await sut.GetByIdAsync(testId);
        }
        catch (EntityNotFoundException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeTrue();
    }

    [Fact]
    public async Task GetAllAsync_HasAppointments_True()
    {
        // Arrange
        var dbSetMock = AppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);

        // Act
        var sut = new AppointmentRepository(context.Object);
        var result = await sut.GetAllAsync();
            
        // Assert
        result.Should().BeEquivalentTo(AppointmentsList.ToList());
    }
    
    [Fact]
    public async Task GetAllAsync_HasAppointments_False()
    {
        // Arrange
        var dbSetMock = EmptyAppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);

        // Act
        var sut = new AppointmentRepository(context.Object);
        var result = await sut.GetAllAsync();
            
        // Assert
        EmptyAppointmentsList.Should().BeEquivalentTo(result.ToList());
    }

    [Fact]
    public async Task GetByDateAsync_ExistingDate_True()
    {
        // Arrange
        var expectedResult = new List<Appointment>
        {
            AppointmentsList[1],
            AppointmentsList[2]
        };
        var testAppDate = DateTime.Parse("2022-03-01");
        
        var dbSetMock = AppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        var result = await sut.GetByDateAsync(testAppDate);
        
        // Assert
        result.ToList().Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetByBranchAsync()
    {
        // Arrange
        var expectedResult = new List<Appointment> { AppointmentsList[1], AppointmentsList[4] };
        const int testBranchId = 2;
        
        var dbSetMock = AppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        var result = await sut.GetByBranchAsync(testBranchId);
        
        // Assert
        result.ToList().Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetByDateAndBarberAsync()
    {
        // Arrange
        var expectedResult = new List<Appointment> { AppointmentsList[6], AppointmentsList[7] };
        
        var testAppDate = DateTime.Parse("2022-03-15");
        const int barberId = 6;
        
        var dbSetMock = AppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        var result = await sut.GetByDateAndBarberAsync(testAppDate, barberId);
        
        // Assert
        result.ToList().Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetBarbersAppointmentsAsync()
    {
        // Arrange
        var expectedResult = new List<Appointment> { AppointmentsList[3], AppointmentsList[6], AppointmentsList[7] };
        
        const int barberId = 6;
        
        var dbSetMock = AppointmentsList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        var result = await sut.GetBarbersAppointmentsAsync(barberId);
        
        // Assert
        result.ToList().Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task InsertAsync()
    {
        // Arrange
        var testAppointment = new Appointment
        {
            Id = 9,
            BarberUserId = 3,
            CustomerUserId = 18,
            AppointmentStatusId = 1,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-16"),
            BeginTime = TimeSpan.Parse("13:00:00"),
            EndTime = TimeSpan.Parse("13:45:00")
        };
        var expectedResult = new List<Appointment> { testAppointment };
        
        var dbSetMock = new Mock<DbSet<Appointment>>();
        var context = new Mock<BarbershopDbContext>();
        
        dbSetMock
            .Setup(_ => _.AddAsync(It.IsAny<Appointment>(), It.IsAny<CancellationToken>()))
            .Callback((Appointment model, CancellationToken _) => { AppointmentsList.Add(model); })
            .Returns(null);
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        await sut.InsertAsync(testAppointment);
        var result = AppointmentsList;
        
        // Assert
        result.Last().Should().BeEquivalentTo(expectedResult.Last());
    }
    
    [Fact]
    public async Task UpdateAsync()
    {
        // Arrange
        var testAppointment = new Appointment
        {
            Id = 4,
            BarberUserId = 5,
            CustomerUserId = 17,
            AppointmentStatusId = 2,
            BranchId = 1,
            AppDate = DateTime.Parse("2022-03-12"),
            BeginTime = TimeSpan.Parse("16:15:00"),
            EndTime = TimeSpan.Parse("17:15:00")
        };
        var expectedResult = testAppointment;
        
        var dbSetMock = new Mock<DbSet<Appointment>>();
        var context = new Mock<BarbershopDbContext>();

        dbSetMock
            .Setup(_ => _.Update(It.IsAny<Appointment>()))
            .Callback((Appointment model) =>
            {
                var desiredIndex = AppointmentsList.FindIndex(x => x.Id == testAppointment.Id);
                AppointmentsList[desiredIndex] = model;
            });
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        await sut.UpdateAsync(testAppointment);
        var result = AppointmentsList[3];
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task DeleteByIdAsync_ExistingId_True()
    {
        // Arrange
        var appointmentListCopy = AppointmentsList.Select(appointment => new Appointment(appointment)).ToList();
        const int testAppointmentId = 4;
        var expectedResult = appointmentListCopy.Where(_ => _.Id != testAppointmentId).ToList();

        var dbSetMock = appointmentListCopy.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();

        dbSetMock
            .Setup(x => x.Remove(It.IsAny<Appointment>()))
            .Callback<Appointment>((entity) => appointmentListCopy.Remove(entity));
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        await sut.DeleteByIdAsync(testAppointmentId);
        var result = appointmentListCopy;
        
        // Assert
        result.Last().Should().BeEquivalentTo(expectedResult.Last());
    }
    
    [Fact]
    public async Task DeleteByIdAsync_ExistingId_False()
    {
        // Arrange
        var appointmentListCopy = AppointmentsList.Select(appointment => new Appointment(appointment)).ToList();
        const int testAppointmentId = 35;

        var dbSetMock = appointmentListCopy.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();

        dbSetMock
            .Setup(x => x.Remove(It.IsAny<Appointment>()))
            .Callback<Appointment>((entity) => appointmentListCopy.Remove(entity));
        context.Setup(x => x.Set<Appointment>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new AppointmentRepository(context.Object);
        var exceptionThrown = false;
        try
        {
            await sut.DeleteByIdAsync(testAppointmentId);
        }
        catch (EntityNotFoundException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeTrue();
    }
}