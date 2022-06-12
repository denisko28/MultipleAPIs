using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace HR_BLL.Helpers
{
    public static class UserClaimsHelper
    {
        public static int GetUserId(HttpContext httpContext)
        {
            if (httpContext.User.Identity is not ClaimsIdentity identity) 
                return -1;
            
            var userIdStr = identity.Claims
                .FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdStr, out var userId))
                throw new Exception("Proper UserId claim wasn't found");

            return userId;
        }
        
        public static UserClaimsModel GetUserClaims(HttpContext httpContext)
        {
            if (httpContext.User.Identity is not ClaimsIdentity identity) 
                throw new Exception("User has no identity claims");

            var userClaims = identity.Claims.ToList();

            var nameIdentifier = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(nameIdentifier, out var userId))
                throw new Exception("Proper UserId claim wasn't found");
            
            return new UserClaimsModel
            {
                UserId = userId,
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            };
        }
    }
}