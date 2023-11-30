using System.Security.Cryptography;
using System.Text;

namespace Game1.Web.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit key
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Optionally, you can generate a random salt for each user and store it alongside the hashed password
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16]; // 16 bytes for a 128-bit salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword, string storedSalt)
        {
            string enteredPasswordHashed = HashPassword(enteredPassword, storedSalt);
            return enteredPasswordHashed == storedHashedPassword;
        }
    }
}
