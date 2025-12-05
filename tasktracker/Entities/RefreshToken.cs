using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tasktracker.Entities
{
    /// <summary>
    /// Refresh token entity class
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Refresh token identity
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Token value
        /// </summary>
        public required string Token { get; set; }

        /// <summary>
        /// Associated user ID
        /// </summary>
        public required int UserId { get; set; }

        /// <summary>
        /// Expiration date-time
        /// </summary>
        public required DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Creation date-time
        /// </summary>
        public required DateTime CreatedAt { get; set; }

        /// <summary>
        /// IP that created the token
        /// </summary>
        public required string CreatedByIp { get; set; }

        /// <summary>
        /// Revokation date and time
        /// </summary>
        public DateTime? RevokedAt { get; set; }

        /// <summary>
        /// IP that revoked the token
        /// </summary>
        public string? RevokedByIp { get; set; }

        /// <summary>
        /// Indicate if the token is revoked
        /// </summary>
        public required bool IsRevoked { get; set; }

        /// <summary>
        /// Indicate revocation reason
        /// </summary>
        public string? RevokedReason { get; set; }

        /// <summary>
        /// Token value replacement on rotation
        /// </summary>
        public string? ReplacedByToken { get; set; }

    }
}
