using SmartCqrs.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Security.Claims;
using SmartCqrs.Infrastructure.Log;
using SmartCqrs.API.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace SmartCqrs.API.Controllers
{
    [Authorize]
    public class AuthController : ControllerBase
    {
        protected readonly ILoggerManager _logger;

        public AuthController()
        {
            // 通过 HttpContext.RequestServices.GetService<T>() 方法只能获取到生命周期类型为 Scoped 的实例，无法获取Transient和Singleton实例
            //_logger = HttpContext?.RequestServices.GetService<ILoggerManager>();

            _logger = ServiceLocator.Instance.GetService<ILoggerManager>();
        }

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