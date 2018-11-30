using System.Threading.Tasks;
using SmartCqrs.Application.Commands;
using SmartCqrs.Infrastructure.CommonServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartCqrs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : AuthController
    {
        private readonly IMediator _mediator;
        private readonly TongHangBrokerAuthServiceClient _authClient;

        public CommonController(IMediator mediator, TongHangBrokerAuthServiceClient authClient)
        {
            _mediator = mediator;
            _authClient = authClient;
        }


        /// <summary>
        /// 刷新用户token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh/usertoken")]
        public async Task<IActionResult> RefreshUserToken()
        {
            var cmdResult = await _authClient.RefreshUserToken(ClientUser.AccessToken);
            return Ok(cmdResult);
        }

    }
}