using System;
using System.Data;

namespace HR_DAL.Connection.Abstract
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();

        string GetConnectionString();
    }
}
