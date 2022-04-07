using System;
using System.Data;

namespace MultipleAPIs.HR_DAL.Connection.Abstract
{
    public interface IConnectionFactory:IDisposable
    {
        IDbConnection Connect { get; }

        String GetConnectionString();

    }
}
