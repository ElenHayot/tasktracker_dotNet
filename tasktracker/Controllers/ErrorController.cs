using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tasktracker.Exceptions;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Error controller - manage errors
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    // api/error
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Handle throwed errors
        /// </summary>
        /// <returns>An error</returns>
        [HttpGet]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context?.Error is NotFoundException)
            {
                return Problem(statusCode: 404, title: context.Error.Message);
            }

            return Problem(statusCode: 500, title: "An unexpected error occured.");
        }
    }
}
