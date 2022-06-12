using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Customers_DAL.Entities;

namespace Customers_BLL.Factories.Abstract
{
    public interface IJwtSecurityTokenFactory
    {
        JwtSecurityToken BuildToken(User user, IEnumerable<string> roles);
    }
}
