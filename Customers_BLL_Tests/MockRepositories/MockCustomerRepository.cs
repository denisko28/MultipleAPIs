using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Moq;

namespace Customers_BLL_Tests.MockRepositories;

public class MockCustomerRepository: Mock<ICustomerRepository>
{
    public MockCustomerRepository MockGetByIdAsync(Customer result)
    {
        Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(result);
        return this;
    }
    
    public MockCustomerRepository MockGetAllAsync(IEnumerable<Customer> result)
    {
        Setup(x => x.GetAllAsync())
            .ReturnsAsync(result);
        return this;
    }
    
    public MockCustomerRepository MockInsertAsync()
    {
        Setup(x => x.InsertAsync(It.IsAny<Customer>()));
        return this;
    }
    
    public MockCustomerRepository MockUpdateAsync()
    {
        Setup(x => x.UpdateAsync(It.IsAny<Customer>()));
        return this;
    }
    
    public MockCustomerRepository MockDeleteByIdAsync()
    {
        Setup(x => x.DeleteByIdAsync(It.IsAny<int>()));
        return this;
    }
}