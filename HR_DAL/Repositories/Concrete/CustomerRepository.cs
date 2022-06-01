using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Exceptions;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly IDbConnection Connection;

        public CustomerRepository(IConnectionFactory connectionFactory)
        {
            Connection = connectionFactory.GetConnection();
            Connection.Open();
        }
        
        public async Task<Customer> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Customer WHERE UserId=@Id";
            var values = new { Id = id };
            var result = await Connection.QuerySingleAsync<Customer>(sql, values);
            
            if(result == null)
                throw new EntityNotFoundException(nameof(Customer), id);
            
            return result;
        }
        
        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
