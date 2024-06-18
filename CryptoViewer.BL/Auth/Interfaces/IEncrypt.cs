using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Auth.Interfaces
{
    /// <summary>
    /// Interface defining operations for password hashing.
    /// </summary>
    public interface IEncrypt
    {
        /// <summary>
        /// Hashes the provided password using the specified salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use for hashing.</param>
        /// <returns>The hashed password as a string.</returns>
        string HashPassword(string password, string salt);
    }
}