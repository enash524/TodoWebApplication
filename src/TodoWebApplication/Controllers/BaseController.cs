using MediatR;
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

        private IMediator _mediator;

        protected ILogger<BaseController> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<BaseController>>();

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
