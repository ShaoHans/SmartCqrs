using SmartCqrs.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;

namespace SmartCqrs.API.Controllers
{
    [Authorize(Policy = "user")]
    public class AuthController : ControllerBase
    {
        public ClientUser ClientUser
        {
            get
            {
                //if (HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr))
                //{
                //    var user = JWTHelper.GetClientUser(authStr.ToString());
                //    user.AccessToken = authStr;
                //    return user;
                //}
                //return null;
                return new ClientUser { UUID = Guid.Parse("745a0510-a80e-4804-94b4-f68f1bc45e69") };
            }
        }
    }
}