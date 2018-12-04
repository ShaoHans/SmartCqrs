using System;
using System.Collections.Generic;
using System.Text;
using SmartCqrs.Infrastructure.Auth;
using System.Linq;
using IdentityModel;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static ClientUser ToClientUser(this ClaimsPrincipal user)
        {
            if (user == null || user.Claims == null)
            {
                return null;
            }
            //var subClaim = user.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject);
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Id);
            return new ClientUser
            {
                //Sub = subClaim.Value,
                UUID = userIdClaim.Value.ToGuid()
            };
        }
    }
}
