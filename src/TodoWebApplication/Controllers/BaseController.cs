using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TodoWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private ILogger<BaseController> _logger;

        protected ILogger<BaseController> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<BaseController>>();
    }
}
