using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using tasktracker.DtoModels;
using tasktracker.Exceptions;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Auth controller - manage authentication part
    /// </summary>
    // [Route("api/v1/auth")]
    [ApiController]
    [Route("api/v1/[Controller]")]  // api/v1/Auth
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
        /// Call AuthService.LoginUserAsync
        /// </summary>
        /// <param name="loginDto">FromBody parameter with login infos</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                string ipAddress = _authService.GetClientIp(HttpContext);
                LoginServiceResponseDto responseSvc = await _authService.LoginUserAsync(loginDto, ipAddress);

                //Store the refresh token in a httpOnly cookie
                Response.Cookies.Append("refreshToken", responseSvc.RefreshToken.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // false => http/https; true => HTTPS only
                    SameSite = SameSiteMode.None, // cross-origin : different ports between server and front
                    Expires = responseSvc.RefreshToken.ExpiresAt,
                    Path = "/"  // "All routes" access
                });

                return Ok(responseSvc.ResponseDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(WrongPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Call AuthService.RefreshAsync
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                if (refreshToken != null)
                {
                    var response = await _authService.RefreshAsync(refreshToken);
                    return Ok(response);
                } else
                {
                    return Unauthorized("Expired cookie");
                }
                    
            }
            catch (NotFoundException ex)
            {
                Response.Cookies.Delete("refreshToken");
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.Cookies.Delete("refreshToken");
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Call AuthService.LogoutUserAsync
        /// </summary>
        /// <param name="refreshToken">FromBody refresh token value</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            string ipAddress = _authService.GetClientIp(HttpContext);
            await _authService.LogoutUserAsync(refreshToken, ipAddress);
            return NoContent();
        }

        /// <summary>
        /// Call AuthService.LogoutFromAllDevicesAsync
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("logout-all")]
        public async Task<IActionResult> LogoutFromAllDevices()
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            string ipAddress = _authService.GetClientIp(HttpContext);

            await _authService.LogoutFromAllDevicesAsync(userId, ipAddress);
            return NoContent();
        }

        /// <summary>
        /// Get me
        /// </summary>
        /// <returns>Me</returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                //var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                UserDto user = await _authService.GetCurrentUserAsync(userId);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
