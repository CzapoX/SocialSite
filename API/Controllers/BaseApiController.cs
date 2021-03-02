using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices
            .GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            
            switch(result.ResultStatus)
            {
                case ResultStatus.IsUnauthorized:
                    return Unauthorized(result.Error);

                case ResultStatus.IsSuccess:
                    if (result.Value == null)
                        return NotFound();
                    else
                        return Ok(result.Value);

                case ResultStatus.Error:
                    return BadRequest(result.Error);
                default:
                    return BadRequest();
            }
        }
    }
}