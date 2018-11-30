using System.Diagnostics;
using SmartCqrs.Infrastructure.Auth;
using SmartCqrs.Infrastructure.Log;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SmartCqrs.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerManager _logger;
        private readonly CommonserviceUrlModel _commonserviceUrlModel;
        private readonly IServiceAuthorization _serviceAuthorization;
        public ValuesController(IMediator mediator, ILoggerManager logger, 
            IOptionsSnapshot<CommonserviceUrlModel> commonServer, IServiceAuthorization serviceAuthorization)
        {
            _serviceAuthorization = serviceAuthorization;
            _commonserviceUrlModel = commonServer.Value;
            _mediator = mediator;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            Debug.WriteLine(_serviceAuthorization.Token);
            return Ok(_serviceAuthorization.Token);
        }
        
        [HttpGet,Route("get2")]
        [MapToApiVersion("2.0")]
        [AllowAnonymous]
        public IActionResult Get2()
        {
            return Ok("v2");
        }

        // GET api/values/5
        [HttpGet("{id}")]        
        public ActionResult<string> Get(int id)
        {
            return "";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
