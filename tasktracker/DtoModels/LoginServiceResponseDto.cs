using tasktracker.Entities;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// Login response DTO sent by a service
    /// </summary>
    public class LoginServiceResponseDto
    {
        /// <summary>
        /// LoginServiceResponseDto constructor
        /// </summary>
        /// <param name="refreshToken">Refresh token object</param>
        /// <param name="responseDto">Login response dto object</param>
        public LoginServiceResponseDto(RefreshToken refreshToken, LoginResponseDto responseDto)
        {
            RefreshToken = refreshToken;
            ResponseDto = responseDto;
        }

        /// <summary>
        /// Refresh token containing all token's informations
        /// </summary>
        public RefreshToken RefreshToken { get; set; } = default!;

        /// <summary>
        /// Response DTO sent back to the client by a controller
        /// </summary>
        public LoginResponseDto ResponseDto { get; set; } = default!;

    }
}
