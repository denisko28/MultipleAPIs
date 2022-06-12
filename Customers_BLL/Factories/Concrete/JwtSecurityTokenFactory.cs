using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Customers_BLL.Configurations;
using Customers_BLL.Factories.Abstract;
using Customers_DAL.Entities;

namespace Customers_BLL.Factories.Concrete
{
    public class JwtSecurityTokenFactory : IJwtSecurityTokenFactory
    {
        private readonly JwtTokenConfiguration jwtTokenConfiguration;

        public JwtSecurityTokenFactory(JwtTokenConfiguration jwtTokenConfiguration)
        {
            this.jwtTokenConfiguration = jwtTokenConfiguration;
        }

        public JwtSecurityToken BuildToken(User user, IEnumerable<string> roles) => new(
            issuer: jwtTokenConfiguration.Issuer,
            audience: jwtTokenConfiguration.Audience,
            claims: GetClaims(user, roles),
            expires: JwtTokenConfiguration.ExpirationDate,
            signingCredentials: jwtTokenConfiguration.Credentials);

        private static IEnumerable<Claim> GetClaims(User user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>()
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.GivenName, user.FirstName),
                new (ClaimTypes.Surname, user.LastName)
            };
            
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
