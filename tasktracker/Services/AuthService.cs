using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tasktracker.Common;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Exceptions;
using tasktracker.Mappers;
using tasktracker.Repositories;

namespace tasktracker.Services
{
    /// <summary>
    /// Authentication service - manage JWTs creation
    /// </summary>
    public class AuthService : IAuthService
    {
        #region Instancies
        /// <summary>
        /// Local appsettings instance
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Local user repository instance
        /// </summary>
        private readonly IUserRepository _userRepository;
        #endregion

        /// <summary>
        /// AuthService constructor
        /// </summary>
        /// <param name="config">Appsettings instance</param>
        /// <param name="userRepository">User repository instance</param>
        public AuthService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        #region Public methods

        /// <inheritdoc/>
        public async Task<(string Token, UserDto User)> LoginUser(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            // Verify email
            if (user == null)
            {
                throw new NotFoundException($"User with email '{loginDto.Email}' not found !");
            }
            // Verify password
            if (!PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new WrongPasswordException("Bad password - please verify your password");
            }

            // Generate token
            string token = GenerateJwtToken(user);

            return (token, UserMapper.ToDto(user));
        }

        /// <inheritdoc/>
        public string GenerateJwtToken(UserEntity user)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // JWT standard identifier
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   // .Net identifier (optionnal)
                new Claim(ClaimTypes.Name, user.Email),                     // usually email or pseudo
                new Claim(ClaimTypes.Role, user.Role.ToString())            // user role for authorization
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

    }
}
