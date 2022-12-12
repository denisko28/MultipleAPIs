using AutoMapper;
using Customers_BLL_Tests.MockRepositories;
using Customers_BLL.Configurations;
using Customers_BLL.DTO.Requests;
using Customers_BLL.Exceptions;
using Customers_DAL.UnitOfWork.Abstract;
using FluentAssertions;
using IdentityServer.Helpers;
using Moq;
using CustomerService = Customers_BLL.Services.Concrete.CustomerService;

namespace Customers_BLL_Tests;

public class CustomerServiceTest
{
    private readonly IMapper _mapper;
    
    public CustomerServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task InsertAsync()
    {
        var testCustomer = new CustomerRequest
        {
            UserId = 16,
            VisitsNum = 0
        };
        var mockCustomerRepository = new MockCustomerRepository().MockInsertAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.CustomerRepository)
            .Returns(mockCustomerRepository.Object);
        
        // Act
        var sut = new CustomerService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.InsertAsync(testCustomer);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_AsAdmin()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Admin};
        var testCustomer = new CustomerRequest
        {
            UserId = 16,
            VisitsNum = 5
        };
        var mockCustomerRepository = new MockCustomerRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.CustomerRepository)
            .Returns(mockCustomerRepository.Object);
        
        // Act
        var sut = new CustomerService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testCustomer, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_AsCustomer_Accessible_True()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Customer, UserId = 16};
        var testCustomer = new CustomerRequest
        {
            UserId = 16,
            VisitsNum = 5
        };
        var mockCustomerRepository = new MockCustomerRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.CustomerRepository)
            .Returns(mockCustomerRepository.Object);
        
        // Act
        var sut = new CustomerService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testCustomer, testClaimsModel);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_AsCustomer_Accessible_False()
    {
        // Arrange
        var testClaimsModel = new UserClaimsModel {Role = UserRoles.Customer, UserId = 11};
        var testCustomer = new CustomerRequest
        {
            UserId = 16,
            VisitsNum = 5
        };
        var mockCustomerRepository = new MockCustomerRepository().MockUpdateAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.CustomerRepository)
            .Returns(mockCustomerRepository.Object);
        
        // Act
        var sut = new CustomerService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.UpdateAsync(testCustomer, testClaimsModel);
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
        const int testCustomerId = 4;
        var mockCustomerRepository = new MockCustomerRepository().MockDeleteByIdAsync();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.SetupGet(x => x.CustomerRepository)
            .Returns(mockCustomerRepository.Object);
        
        // Act
        var sut = new CustomerService(mockUnitOfWork.Object, _mapper);
        var exceptionThrown = false;
        try
        {
            await sut.DeleteByIdAsync(testCustomerId);
        }
        catch (ForbiddenAccessException)
        {
            exceptionThrown = true;
        }

        // Assert
        exceptionThrown.Should().BeFalse();
    }
}