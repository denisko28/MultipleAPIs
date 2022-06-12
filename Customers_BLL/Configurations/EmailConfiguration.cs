using Microsoft.Extensions.Configuration;

namespace Customers_BLL.Configurations
{
    public class EmailConfiguration
    {
        private readonly IConfiguration configuration;
        
        public EmailConfiguration(IConfiguration configuration) => this.configuration = configuration;
        
        public string From => configuration["EmailConfiguration:From"];
        
        public string SmtpServer => configuration["EmailConfiguration:SmtpServer"];
        
        public int Port => int.Parse(configuration["EmailConfiguration:Port"]);
        
        public string UserName => configuration["EmailConfiguration:Username"];
        
        public string Password => configuration["EmailConfiguration:Password"];
    }
}