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

        /// <summary>
        /// Local refresh token service instance
        /// </summary>
        private readonly IRefreshTokenService _refreshTokenService;
        #endregion

        /// <summary>
        /// AuthService constructor
        /// </summary>
        /// <param name="config">Appsettings instance</param>
        /// <param name="userRepository">User repository instance</param>
        /// <param name="refreshTokenService">Refresh token service instance</param>
        public AuthService(IConfiguration config, IUserRepository userRepository, IRefreshTokenService refreshTokenService)
        {
            _config = config;
            _userRepository = userRepository;
            _refreshTokenService = refreshTokenService;
        }

        #region Public methods
        /// <inheritdoc/>
        public async Task<LoginServiceResponseDto> LoginUserAsync(UserLoginDto loginDto, string ipAddress)
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
            string accessToken = GenerateJwtToken(user);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.Id, ipAddress);
            LoginResponseDto responseDto = new() { AccessToken = accessToken, User = UserMapper.ToDto(user) };

            return new LoginServiceResponseDto(refreshToken, responseDto);
        }

        /// <inheritdoc/>
        public async Task<LoginResponseDto> RefreshAsync(string refreshToken)
        {
            var storedRefreshToken = await _refreshTokenService.GetByTokenAsync(refreshToken);

            if (storedRefreshToken == null || storedRefreshToken.IsRevoked || storedRefreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            UserEntity? user = await _userRepository.GetUserByIdAsync(storedRefreshToken.UserId);
            if (user == null) { throw new NotFoundException($"No user with ID '{storedRefreshToken.UserId}' found."); }
            
            string newAccessToken = GenerateJwtToken(user);

            return new LoginResponseDto()
            {
                AccessToken = newAccessToken,
                User = UserMapper.ToDto(user)
            };
        }

        /// <inheritdoc/>
        public async Task LogoutUserAsync(string refreshToken, string ipAddress)
        {
            await _refreshTokenService.RevokeAsync(refreshToken, ipAddress, "Logout user");
        }

        /// <inheritdoc/>
        public async Task LogoutFromAllDevicesAsync(int userId, string ipAddress)
        {
            await _refreshTokenService.RevokeAllUserTokensAsync(userId, ipAddress, "Logout user from all devices");
        }

        /// <inheritdoc/>
        public async Task<UserDto> GetCurrentUserAsync(string? userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Token user ID is null");
            }

            if (!int.TryParse(userId, out var id)) {
                throw new UnauthorizedAccessException("Invalid user ID in token");
            }

            UserEntity? entity = await _userRepository.GetUserByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"User ID in token does not exist anymore");
            }

            return UserMapper.ToDto(entity);
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

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

        /// <inheritdoc/>
        public string GetClientIp(HttpContext context)
        {
            // If X-Forwardeed-For in header : take first IP address (client IP address)
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwarded)){
                var ip = forwarded.ToString().Split(',').First().Trim();
                if (!String.IsNullOrEmpty(ip)) { return ip; }
            }

            // if X-Real-IP in headers (useer by NGINX)
            if (context.Request.Headers.TryGetValue("X-Real-IP", out var realIp))
            {
                return realIp!;
            }

            // Fallback -> client or proxy IP address
            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }
        #endregion

    }
}
