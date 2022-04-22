using System;
using System.Data;

namespace HR_DAL.Connection.Abstract
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Connect { get; }

        string GetConnectionString();
    }
}
