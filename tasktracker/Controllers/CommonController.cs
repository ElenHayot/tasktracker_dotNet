using Microsoft.AspNetCore.Mvc;
using tasktracker.Enums

namespace tasktracker.Controllers
{
    public class CommonController : ControllerBase
    {
        public CommonController() { }

        [HttpGet("/roles")]
        public IActionResult GetRoles()
        {
            var roles = Enum.GetNames(typeof(RolesEnum));
            return Ok(roles);
        }

        [HttpGet("/status")]
        public IActionResult GetStatus()
        {
            var status = Enum.GetNames(typeof(StatusEnum));
            return Ok(status);
        }
    }
}
