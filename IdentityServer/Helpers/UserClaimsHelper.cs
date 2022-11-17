using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace IdentityServer.Helpers
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

            var idStr = userClaims.FirstOrDefault(o => o.Type == CustomJwtClaimTypes.UserId)?.Value;
            if (!int.TryParse(idStr, out var userId))
                throw new Exception("Proper UserId claim wasn't found");

            var branchIdStr = userClaims.FirstOrDefault(o => o.Type == CustomJwtClaimTypes.BranchId)?.Value;
            var branchId = int.TryParse(branchIdStr, out var f) ? f : -1;
            
            return new UserClaimsModel
            {
                UserId = userId,
                Email = userClaims.FirstOrDefault(o => o.Type == CustomJwtClaimTypes.Email)?.Value,
                GivenName = userClaims.FirstOrDefault(o => o.Type == CustomJwtClaimTypes.FirstName)?.Value,
                FamilyName = userClaims.FirstOrDefault(o => o.Type == CustomJwtClaimTypes.LastName)?.Value,
                BranchId = branchId,
                Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            };
        }
    }
}