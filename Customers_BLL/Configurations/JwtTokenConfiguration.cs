using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Customers_BLL.Configurations
{
    public class JwtTokenConfiguration
    {
        private readonly IConfiguration configuration;
        
        public JwtTokenConfiguration(IConfiguration configuration) => this.configuration = configuration;

        public string Issuer => configuration["JWT:ValidIssuer"];

        public string Audience => configuration["JWT:ValidAudience"];

        public static DateTime ExpirationDate => DateTime.UtcNow.AddHours(5);

        private SymmetricSecurityKey Key => new(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

        public SigningCredentials Credentials => new(Key, SecurityAlgorithms.HmacSha256);
    }
}