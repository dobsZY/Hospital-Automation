using System;
using System.Security.Cryptography;
using System.Text;

namespace HospitalAutomation.Utilities
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16; // bytes
        private const int KeySize = 32;  // bytes
        private const int Iterations = 100000;
        private const string LegacySalt = "HospitalSalt2024"; // eski yöntemi tanýmak/verifikasyon için

        /// <summary>
        /// PBKDF2 ile hash üretir. Format: {iterations}.{saltBase64}.{hashBase64}
        /// </summary>
        public static string HashPassword(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                var key = pbkdf2.GetBytes(KeySize);
                return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
            }
        }

        /// <summary>
        /// Parolayý storedHash ile doðrular. Eski SHA256+fixed-salt formatý da desteklenir.
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(storedHash)) return false;

            try
            {
                var parts = storedHash.Split('.');
                if (parts.Length == 3)
                {
                    // Yeni PBKDF2 formatý
                    int iterations = int.Parse(parts[0]);
                    var salt = Convert.FromBase64String(parts[1]);
                    var expectedKey = Convert.FromBase64String(parts[2]);

                    using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                    {
                        var actualKey = pbkdf2.GetBytes(expectedKey.Length);
                        return FixedTimeEquals(actualKey, expectedKey);
                    }
                }
                else
                {
                    // Muhtemel legacy SHA256(base + fixed salt) formatý
                    using (var sha256 = SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + LegacySalt));
                        var hashedBase64 = Convert.ToBase64String(hashedBytes);
                        return string.Equals(hashedBase64, storedHash, StringComparison.Ordinal);
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Eðer storedHash legacy formatýndaysa veya düþük iterasyon içeriyorsa yeniden hashlenmeli.
        /// </summary>
        public static bool NeedsRehash(string storedHash)
        {
            if (string.IsNullOrWhiteSpace(storedHash)) return true;

            var parts = storedHash.Split('.');
            if (parts.Length != 3) return true; // legacy veya bilinmeyen format -> rehash öner
            try
            {
                int iterations = int.Parse(parts[0]);
                if (iterations < Iterations) return true;
                return false;
            }
            catch
            {
                return true;
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }
    }
}