using System.Threading.Tasks;
using SmartCqrs.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartCqrs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : AuthController
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm]CreateOrderCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("close")]
        public async Task<IActionResult> Delete([FromForm]DeleteOrderCommand command)
        {
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }
    }
}