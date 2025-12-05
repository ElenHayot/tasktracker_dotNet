namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model send back when login
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Created access token
        /// </summary>
        public string AccessToken { get; set; } = default!;

        /// <summary>
        /// Created refresh token
        /// </summary>
        public string RefreshToken { get; set; } = default!;

        /// <summary>
        /// Corresponding user
        /// </summary>
        public UserDto User { get; set; } = default!;
    }
}
