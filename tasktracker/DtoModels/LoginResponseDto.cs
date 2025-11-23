namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model send back when login
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Created token
        /// </summary>
        public string Token { get; set; } = default!;

        /// <summary>
        /// Corresponding user
        /// </summary>
        public UserDto User { get; set; } = default!;
    }
}
