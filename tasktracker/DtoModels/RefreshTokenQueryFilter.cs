namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO query filter model for RefreshToken
    /// </summary>
    public class RefreshTokenQueryFilter
    {
        /// <summary>
        /// Filter on Token field
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Filter on UserId field
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Filter on ExpiresAt field
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Filter on CreatedAt field
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Filter on CreatedByIp field
        /// </summary>
        public string? CreatedByIp { get; set; }

        /// <summary>
        /// Filter on IsRevoked field
        /// </summary>
        public bool? IsRevoked { get; set; }
    }
}
