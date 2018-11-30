using SmartCqrs.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace SmartCqrs.API.Filters
{
    public class CommonAuthorizeHandler : AuthorizationHandler<CommonAuthorize>
    {
        /// <summary>
        /// 常用自定义验证策略
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommonAuthorize requirement)
        {
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;
            #region 身份验证，并设置用户Ruser值

            var result = httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            if (!result || string.IsNullOrEmpty(authStr.ToString()))
            {
                return Task.CompletedTask;
            }
            result = JWTHelper.Validate(authStr.ToString(), payLoad =>
            {
                var success = true;
                //可以添加一些自定义验证，用法参照测试用例
                //验证是否包含aud 并等于 roberAudience
                if (success)
                {
                    //设置Ruse值,把user信息放在payLoad中，（在获取jwt的时候把当前用户存放在payLoad的ruser键中）
                    //如果用户信息比较多，建议放在缓存中，payLoad中存放缓存的Key值
                    //userContext.TryInit(payLoad["ruser"]?.ToString());
                }
                return success;
            });
            if (!result)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            #endregion

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class CommonAuthorize : IAuthorizationRequirement
    {

    }
}
