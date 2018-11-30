using System;
using System.Threading.Tasks;
using SmartCqrs.Application.Commands;
using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.DapperEx;
using SmartCqrs.Infrastructure.Results;
using SmartCqrs.Query.Services;
using SmartCqrs.Query.Requests;
using SmartCqrs.Query.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartCqrs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : AuthController
    {
        private readonly IMediator _mediator;
        private readonly ICarQuery _carQuery;
        private readonly IUserQuery _userQuery;

        public CarController(IMediator mediator, ICarQuery carQuery, IUserQuery userQuery)
        {
            _mediator = mediator;
            _carQuery = carQuery;
            _userQuery = userQuery;
        }

        #region 查询操作

        /// <summary>
        /// 车源分页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paging")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CommandResult<PagedData<CarListVm>>), (int)ResultCode.SUCCESS)]
        public async Task<ActionResult<CommandResult>> GetPagedData([FromQuery]CarSourcePagedRequest request)
        {
            var pagedData = await _carQuery.GetPagedDataAsync(request);
            return Ok(new CommandResult<PagedData<CarListVm>>(data: pagedData));
        }

        /// <summary>
        /// 车源详情
        /// </summary>
        /// <param name="id">车源Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CarDetailVm), (int)ResultCode.SUCCESS)]
        [ProducesResponseType((int)ResultCode.NOT_FOUND)]
        public async Task<IActionResult> GetDetail(int id)
        {
            var carSource = await _carQuery.GetDetailAsync(id);
            CommandResult<dynamic> result;
            if(carSource == null)
            {
                result = new CommandResult<dynamic>(ResultCode.NOT_FOUND, "车源信息不存在");
            }
            else
            {
                var user = await _userQuery.UserFullDetail(carSource.UserId);

                result = new CommandResult<dynamic>(data: new
                {
                    CarSource = carSource,
                    User = new
                    {
                        user.Avatar,
                        user.NickName,
                        user.ProvinceName,
                        user.CityName,
                        user.SellingCarCount
                    }
                });
            }
            return Ok(result);
        }

        #endregion

        #region 发送命令的操作（增删改）

        /// <summary>
        /// 发布/编辑/重新发布车源
        /// </summary>
        /// <remarks>当code=200时，data表示车源Id</remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("publish")]
        public async Task<ActionResult<CommandResult>> Publish([FromForm]PublishCarCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 浏览车源
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("view")]
        [AllowAnonymous]
        public async Task<ActionResult<CommandResult>> View([FromForm]ViewCarCommand command)
        {
            command.LoginUserId = ClientUser == null ? Guid.Empty : ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 收藏车源
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("collect")]
        public async Task<ActionResult<CommandResult>> Collect([FromForm]CollectCarCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 取消收藏车源
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CollectionCancelled")]
        public async Task<ActionResult<CommandResult>> CollectionCancelled([FromForm]CancelCarCollectionCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        /// <summary>
        /// 删除车源
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult<CommandResult>> Delete([FromForm]DeleteCarCommand command)
        {
            command.LoginUserId = ClientUser.UUID;
            var cmdResult = await _mediator.Send(command);
            return Ok(cmdResult);
        }

        
        #endregion
    }
}