using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.BL.Auth.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CryptoViewer.BL.Auth
{
    /// <summary>
    /// Provides methods for password hashing.
    /// </summary>
    public class Encrypt : IEncrypt
    {
        /// <summary>
        /// Hashes a password using PBKDF2 algorithm with HMAC-SHA512.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt value used for hashing.</param>
        /// <returns>The hashed password as a Base64-encoded string.</returns>
        public string HashPassword(string password, string salt)
        {
            // Convert the salt to bytes (if needed)
            byte[] saltBytes = System.Text.Encoding.ASCII.GetBytes(salt);

            // Generate a hash for the password
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                saltBytes,
                KeyDerivationPrf.HMACSHA512,
                5000,
                64
            ));
        }
    }
}