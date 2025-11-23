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
        /// Local user service instance
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// AuthController constructor
        /// </summary>
        /// <param name="authService">Auth service instance</param>
        /// <param name="userService">User service instance</param>
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        /// <summary>
        /// Call AuthService.LoginUser
        /// </summary>
        /// <param name="loginDto">FromBody parameter with login infos</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            LoginResponseDto response = await _authService.LoginUser(loginDto);
            return Ok(response);
        }

        /// <summary>
        /// Get me
        /// </summary>
        /// <returns>Me</returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            //var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserDto user = await _authService.GetCurrentUserAsync(userId);
            return Ok(user);
        }
    }
}
