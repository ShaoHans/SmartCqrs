using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using SmartCqrs.Infrastructure.Auth;

namespace SmartCqrs.API.Filters
{
    public class JwtCustomerAuthorizeMiddleware
    {
        private readonly RequestDelegate next;

        public JwtCustomerAuthorizeMiddleware(RequestDelegate next, string secret, List<string> anonymousPathList)
        {
            #region   设置自定义jwt 的秘钥
            if (!string.IsNullOrEmpty(secret))
            {
                JWTHelper.SecurityKey = secret;
            }
            #endregion
            this.next = next;
            JWTHelper.AllowAnonymousPathList.AddRange(anonymousPathList);
        }

        public async Task Invoke(HttpContext context, JWTHelper userContext)
        {
            //if (userContext.IsAllowAnonymous(context.Request.Path))
            //{
            //    await next(context);
            //    return;
            //}

            //var option = optionContainer.Value;

            #region 身份验证，并设置用户Ruser值

            var result = context.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            if (!result || string.IsNullOrEmpty(authStr.ToString()))
            {
                throw new UnauthorizedAccessException("未授权");
            }
            result = JWTHelper.Validate(authStr.ToString().Substring("Bearer ".Length).Trim(), payLoad =>
            {
                var success = true;
                //可以添加一些自定义验证，用法参照测试用例                
                return success;
            });
            if (!result)
            {
                throw new UnauthorizedAccessException("未授权");
            }

            #endregion
            #region 权限验证
            //if (!userContext.Authorize(context.Request.Path))
            //{
            //    throw new UnauthorizedAccessException("未授权");
            //}
            #endregion

            await next(context);
        }
    }
}
