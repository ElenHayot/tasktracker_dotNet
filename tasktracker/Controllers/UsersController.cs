using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Enums;
using tasktracker.Exceptions;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// User controller - manage Users part
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]  //api/v1/users
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Local user service istance
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// UsersController constructor
        /// </summary>
        /// <param name="userService">User service instance</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Call Service.GetAllUsersFilteredAsync
        /// </summary>
        /// <param name="name">FromQuery parameter 'name' - string</param>
        /// <param name="firstname">FromQuery parameter 'firstname' - string</param>
        /// <param name="phone">FromQuery parameter 'phone' - string</param>
        /// <param name="role">FromQuery parameter 'role' - Enum value</param>
        /// <returns>List of users</returns>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllUsersFiltered([FromQuery] string? name, [FromQuery] string? firstname, [FromQuery] string? phone, [FromQuery] RolesEnum? role)
        {
            return await _userService.GetAllUsersFilteredAsync(name, firstname, phone, role);
        }

        /// <summary>
        /// Call Service.GetUserByIdAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <returns>One user</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Call Service.CreateUserAsync
        /// </summary>
        /// <param name="userDto">FromBody object</param>
        /// <returns>Ok/Nok result</returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto userDto)
        {
            try
            {
                UserDto user = await _userService.CreateUserAsync(userDto);
                return Ok(user);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Call Service.UpdateUserAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <param name="userDto">FromBody object</param>
        /// <returns>Ok/Nok result</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            try
            {
                UserDto updatedUser = await _userService.UpdateUserAsync(id, userDto);
                return Ok(updatedUser);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Call Service.DeleteUserAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <returns>Ok/Nok result</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
