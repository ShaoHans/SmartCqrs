using SmartCqrs.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Security.Claims;

namespace SmartCqrs.API.Controllers
{
    [Authorize]
    public class AuthController : ControllerBase
    {
        public ClientUser ClientUser
        {
            get
            {
                if(User == null)
                {
                    return null;
                }

                return User.ToClientUser();
                //return new ClientUser { UUID = Guid.Parse("745a0510-a80e-4804-94b4-f68f1bc45e69") };
            }
        }
    }
}