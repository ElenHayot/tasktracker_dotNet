using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using tasktracker.DtoModels;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Auth controller - manage authentication part
    /// </summary>
    [ApiController]
    [Route("api/v1/[Controller]")]  // api/v1/auth
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Local auth service instance
        /// </summary>
        private readonly IAuthService _authService;

        /// <summary>
        /// AuthController constructor
        /// </summary>
        /// <param name="authService">Auth service instance</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Call AuthService.LoginUser
        /// </summary>
        /// <param name="loginDto">FromBody parameter with login infos</param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            return Ok(await _authService.LoginUser(loginDto));
        }

        /// <summary>
        /// Get me
        /// </summary>
        /// <returns>Me</returns>
        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return Ok($"Hello user {userId}");
        } 
    }
}
