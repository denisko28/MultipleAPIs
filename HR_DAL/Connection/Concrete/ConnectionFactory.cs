using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using MultipleAPIs.HR_DAL.Connection.Abstract;

namespace MultipleAPIs.HR_DAL.Connection.Concrete
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connect {
            get
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
                var DbFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var Connection = DbFactory.CreateConnection();
                if (Connection != null)
                {
                    Connection.ConnectionString = @"Data Source=WIN-G0UNAO9UIKB\SQLEXPRESS;Initial Catalog=BarbershopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    Connection.Open();
                }
                return Connection ?? throw new Exception("No connection to the database");
            }
        }

        public void Dispose()
        {
            
        }

        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot Configuration = builder.Build();
            var connectionString = Configuration.GetSection("ConnectionStrings:MSSQLConnection");

            return connectionString.Value;
        }
    }
}
