using SmartCqrs.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace SmartCqrs.API.Controllers
{
    [Authorize(Policy = "user")]
    public class AuthController : ControllerBase
    {
        public ClientUser ClientUser
        {
            get
            {
                if (HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr))
                {
                    var user = JWTHelper.GetClientUser(authStr.ToString());
                    user.AccessToken = authStr;
                    return user;
                }
                return null;
                //return new ClientUser { uuid = Guid.Parse("398e76f8-00e8-451c-844a-2902c32dd71e") };
            }
        }
    }
}