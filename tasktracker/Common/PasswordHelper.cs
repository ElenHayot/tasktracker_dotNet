using System.Security.Cryptography;

namespace tasktracker.Common
{
    /// <summary>
    /// Gestion du password
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// Fonction permettant de générer un password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            // Génère un sel aléatoire
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // Génère un hash PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            // Combine hash + salt
            byte[] hashBytes = new byte[48];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

            // Retourne en base 64
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Fonction permettant de vérifier le password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <returns></returns>
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
