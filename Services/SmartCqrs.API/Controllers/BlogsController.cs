using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCqrs.Application.Commands;

namespace SmartCqrs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : AuthController
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 发布博客
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("publish")]
        public async Task<IActionResult> Publish([FromForm]PublishBlogCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 编辑博客
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromForm]EditBlogCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 收藏博客
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("collect")]
        public async Task<IActionResult> Collect([FromForm]CollectBlogCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 评论博客
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("comment")]
        public async Task<IActionResult> Comment([FromForm]CommentBlogCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }
    }
}