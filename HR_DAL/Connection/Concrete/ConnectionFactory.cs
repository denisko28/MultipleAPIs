using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using HR_DAL.Connection.Abstract;
using Microsoft.Extensions.Configuration;

namespace HR_DAL.Connection.Concrete
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connect {
            get
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
                var dbFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                
                var connection = dbFactory.CreateConnection();
                if (connection == null) 
                    throw new Exception("No connection to the database");

                connection.ConnectionString = GetConnectionString();
                connection.Open();
                return connection ?? throw new Exception("No connection to the database");
            }
        }
        
        public string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            return configuration.GetConnectionString("MSSQLConnection");
        }

        public void Dispose()
        {

        }
    }
}
