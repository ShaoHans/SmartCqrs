namespace SmartCqrs.API.Controllers.V2
{
    /*
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AuthController
    {
        private readonly IMediator _mediator;
        private readonly IUserQuery _userQuery;

        public UsersController(IMediator mediator,
            IUserQuery userQuery)
        {
            _mediator = mediator;
            _userQuery = userQuery;
        }


        /// <summary>
        /// 获取用户签到数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("signin/info")]
        public async Task<IActionResult> GetUserSigninLog()
        {
            var signInLog = await _userQuery.GetUserSigninLog(ClientUser.UUID);
            ; return Ok(new CommandResult(data: new { signInLog, s = "v2" }));
        }
    }*/
}