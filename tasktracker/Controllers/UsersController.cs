using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Enums;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Controller sur la partie Users
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]  //api/v1/users
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Instance d'un IUserService
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="userService">instance d'un IUserService</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Appel de la fonction de récupération de la liste des utilisateurs
        /// </summary>
        /// <param name="name">Filtre possible sur 'name'</param>
        /// <param name="firstname">Filtre possible sur 'firstname'</param>
        /// <param name="role">Filtre possible sur 'role'</param>
        /// <returns>Retour de la liste des utilisateurs correspondants au filtre appliqué</returns>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllUsers([FromQuery] string? name, [FromQuery] string? firstname, [FromQuery] RolesEnum? role)
        {
            return await _userService.GetAllUsersFilteredAsync(name, firstname, role);
        }

        /// <summary>
        /// Appel de la fonction de récupération d'un user par son ID
        /// </summary>
        /// <param name="id">ID en int</param>
        /// <returns>Retour d'un UserDto si existe</returns>
        [HttpGet("{id}")]
        public async Task<UserDto?> GetUserById(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        /// <summary>
        /// Appel de la fonction de création d'utilisateur
        /// </summary>
        /// <param name="userDto">Type UserDto récupéré dans le body</param>
        /// <returns>Retour d'un .ok(), pas du user créé</returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto userDto)
        {
            UserDto user = await _userService.CreateUserAsync(userDto);
            return Ok(user);    // Retourne un ok de réponse mais pas le user créé
        }
    }
}
