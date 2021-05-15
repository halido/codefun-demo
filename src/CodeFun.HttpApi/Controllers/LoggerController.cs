using System.Threading.Tasks;
using CodeFun.LogConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CodeFun.Controllers
{
    [Route("LogConfig")]
    public class LoggerController : CodeFunController
    {
        private readonly ILogConfigAppService _appService;

        public LoggerController(ILogConfigAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(NLogItemEntity), 201)]
        public async Task<IActionResult> Post([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)]
            object model)
        {
            var nLogItemEntity = await _appService.UpsertAsync(model);
            return Created("", nLogItemEntity);
        }
    }
}