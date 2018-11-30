using SmartCqrs.API.ActionResults;
using SmartCqrs.Domain.Exceptions;
using SmartCqrs.Infrastructure.Log;
using SmartCqrs.Infrastructure.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace SmartCqrs.API.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILoggerManager logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILoggerManager logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.Error(context.Exception.Message, context.Exception);

            if (context.Exception.GetType() == typeof(DomainException))
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message.ToString() });

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.Result = new InternalServerErrorObjectResult(new CommandResult(ResultCode.INTERNAL_SERVER_ERROR, "服务器内部出现错误，请稍后重试~"));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}
