using System.Security.Cryptography;

namespace tasktracker.Common
{
    /// <summary>
    /// Password management
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// Hash a password
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <returns>Hash</returns>
        public static string HashPassword(string password)
        {
            // Random salt
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // PBKDF2 hash
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            // hash + salt
            byte[] hashBytes = new byte[48];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

            // Convert to Base64
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verify a password
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <param name="storedHash">Stored hash</param>
        /// <returns>true/false</returns>
        public static bool VerifyPassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i]) return false;
            }
            return true;
        }
    }
}
