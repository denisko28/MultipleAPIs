using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                    Connection.ConnectionString = configuration.GetConnectionString("MSSQLConnection");
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
