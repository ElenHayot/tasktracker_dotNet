using Microsoft.AspNetCore.Mvc;
using tasktracker.Enums;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Common controller - manage common functions
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CommonController : ControllerBase
    {
        /// <summary>
        /// CommonController constructor
        /// </summary>
        public CommonController() { }

        /// <summary>
        /// Get the list of roles
        /// </summary>
        /// <returns>tasktracker.Enums.RolesEnum</returns>
        [HttpGet("/roles")]
        public IActionResult GetRoles()
        {
            var roles = Enum.GetNames(typeof(RolesEnum));
            return Ok(roles);
        }

        /// <summary>
        /// Get the list of status
        /// </summary>
        /// <returns>tasktracker.Enums.StatusEnum</returns>
        [HttpGet("/status")]
        public IActionResult GetStatus()
        {
            var status = Enum.GetNames(typeof(StatusEnum));
            return Ok(status);
        }
    }
}
