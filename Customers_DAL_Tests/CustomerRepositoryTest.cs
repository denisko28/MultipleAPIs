using Customers_DAL;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Concrete;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Customers_DAL_Tests;

public class CustomerRepositoryTest
{
    private static readonly List<Customer> CustomersList = new()
    {
        new Customer
        {
            UserId = 16,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 17,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 18,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 19,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 20,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 21,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 22,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 23,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 24,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 25,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 26,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 27,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 28,
            VisitsNum = 0 
        },
        new Customer
        {
            UserId = 29,
            VisitsNum = 0 
        }
    };

    private static readonly List<Customer> EmptyCustomersList = new();
    
    [Fact]
    public async Task GetByIdAsync_ExistingId_True()
    {
        // Arrange
        var expectedResult = CustomersList[4];
        const int testId = 18;

        var dbSetMock = CustomersList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
        dbSetMock.Setup(x => x.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedResult);
        
        // Act
        var sut = new CustomerRepository(context.Object);
        var result = await sut.GetByIdAsync(testId);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task GetByIdAsync_ExistingId_False()
    {
        // Arrange
        const int testId = 3;

        var dbSetMock = CustomersList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
        dbSetMock.Setup(x => x.FindAsync(It.IsAny<int>()))
            .ReturnsAsync((Customer?) null);
        
        // Act
        var sut = new CustomerRepository(context.Object);
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
    public async Task GetAllAsync_HasCustomers_True()
    {
        // Arrange
        var dbSetMock = CustomersList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);

        // Act
        var sut = new CustomerRepository(context.Object);
        var result = await sut.GetAllAsync();
            
        // Assert
        result.Should().BeEquivalentTo(CustomersList.ToList());
    }
    
    [Fact]
    public async Task GetAllAsync_HasCustomers_False()
    {
        // Arrange
        var dbSetMock = EmptyCustomersList.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);

        // Act
        var sut = new CustomerRepository(context.Object);
        var result = await sut.GetAllAsync();
            
        // Assert
        EmptyCustomersList.Should().BeEquivalentTo(result.ToList());
    }

    [Fact]
    public async Task InsertAsync()
    {
        // Arrange
        var testCustomer = new Customer
        {
            UserId = 30,
            VisitsNum = 0
        };
        var expectedResult = new List<Customer> { testCustomer };
        
        var dbSetMock = new Mock<DbSet<Customer>>();
        var context = new Mock<BarbershopDbContext>();
        
        dbSetMock
            .Setup(_ => _.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .Callback((Customer model, CancellationToken _) => { CustomersList.Add(model); })
            .Returns(null);
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new CustomerRepository(context.Object);
        await sut.InsertAsync(testCustomer);
        var result = CustomersList;
        
        // Assert
        result.Last().Should().BeEquivalentTo(expectedResult.Last());
    }
    
    [Fact]
    public async Task UpdateAsync()
    {
        // Arrange
        var testCustomer = new Customer
        {
            UserId = 28,
            VisitsNum = 5
        };
        var expectedResult = testCustomer;
        
        var dbSetMock = new Mock<DbSet<Customer>>();
        var context = new Mock<BarbershopDbContext>();

        dbSetMock
            .Setup(_ => _.Update(It.IsAny<Customer>()))
            .Callback((Customer model) =>
            {
                var desiredIndex = CustomersList.FindIndex(x => x.UserId == testCustomer.UserId);
                CustomersList[desiredIndex] = model;
            });
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new CustomerRepository(context.Object);
        await sut.UpdateAsync(testCustomer);
        var result = CustomersList[12];
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task DeleteByIdAsync_ExistingId_True()
    {
        // Arrange
        var customerListCopy = CustomersList.Select(customer => new Customer(customer)).ToList();
        const int testCustomerId = 21;
        var expectedEntity = customerListCopy.Find(_ => _.UserId == testCustomerId);
        var expectedResult = customerListCopy.Where(_ => _.UserId != testCustomerId).ToList();

        var dbSetMock = customerListCopy.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();

        dbSetMock
            .Setup(x => x.Remove(It.IsAny<Customer>()))
            .Callback<Customer>((entity) => customerListCopy.Remove(entity));
        dbSetMock.Setup(x => x.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedEntity);
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new CustomerRepository(context.Object);
        await sut.DeleteByIdAsync(testCustomerId);
        var result = customerListCopy;
        
        // Assert
        result.Last().Should().BeEquivalentTo(expectedResult.Last());
    }
    
    [Fact]
    public async Task DeleteByIdAsync_ExistingId_False()
    {
        // Arrange
        var customerListCopy = CustomersList.Select(customer => new Customer(customer)).ToList();
        const int testCustomerId = 5;

        var dbSetMock = customerListCopy.AsQueryable().BuildMockDbSet();
        var context = new Mock<BarbershopDbContext>();

        dbSetMock
            .Setup(x => x.Remove(It.IsAny<Customer>()))
            .Callback<Customer>((entity) => customerListCopy.Remove(entity));
        dbSetMock.Setup(x => x.FindAsync(It.IsAny<int>()))
            .ReturnsAsync((Customer?) null);
        context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
        
        // Act
        var sut = new CustomerRepository(context.Object);
        var exceptionThrown = false;
        try
        {
            await sut.DeleteByIdAsync(testCustomerId);
        }
        catch (EntityNotFoundException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeTrue();
    }
}