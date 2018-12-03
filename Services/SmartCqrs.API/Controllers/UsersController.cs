using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCqrs.Application.Commands;
using SmartCqrs.Application.Dtos;
using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Infrastructure.Results;
using SmartCqrs.Query.Services;
using SmartCqrs.Query.Requests;
using SmartCqrs.Query.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace SmartCqrs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AuthController
    {
        private readonly IMediator _mediator;
        private readonly IUserQuery _userQuery;
        private readonly ICarQuery _carQuery;

        public UsersController(IMediator mediator, 
            IUserQuery userQuery, 
            ICarQuery carQuery)
        {
            _mediator = mediator;
            _userQuery = userQuery;
            _carQuery = carQuery;
        }

        #region 查询

        /// <summary>
        /// 获取指定用户的基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("info/{userId}")]
        [ProducesResponseType(typeof(CommandResult<UserFullDetailVm>), (int)ResultCode.SUCCESS)]
        public async Task<IActionResult> Info(Guid userId)
        {
            var user = await _userQuery.UserFullDetail(userId);
            if(user == null)
            {
                return Ok(new CommandResult(ResultCode.NOT_FOUND, "不存在的用户"));
            }

            return Ok(new CommandResult<UserFullDetailVm>(data: user));
        }

        #endregion

        #region 增删改

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userPatch"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update([FromForm]JsonPatchDocument<UpdateUserInfoDto> userPatch)
        {
            // http://jsonpatch.com/
            var result = await _mediator.Send(new UpdateUserInfoCommand { LoginUserId = ClientUser.UUID, UserPatch = userPatch });
            return Ok(result);
        }

        
        /// <summary>
        /// 设置登录密码
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("setpwd")]
        [AllowAnonymous]
        public async Task<IActionResult> SetLoginPassword([FromForm]SetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 手机短信验证码登录
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("smscode/login")]
        [ProducesResponseType(typeof(CommandResult<LoginDto>), (int)ResultCode.SUCCESS)]
        [AllowAnonymous]
        public async Task<IActionResult> SmsCodeLogin([FromForm]SmsCodeLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("password/login")]
        [ProducesResponseType(typeof(CommandResult<LoginDto>), (int)ResultCode.SUCCESS)]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordLogin([FromForm]PasswordLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion
    }
}