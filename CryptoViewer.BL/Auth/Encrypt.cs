using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.BL.Auth.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CryptoViewer.BL.Auth
{
    public class Encrypt : IEncrypt
    {
        public string HashPassword(string password, string salt)
        {

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                System.Text.Encoding.ASCII.GetBytes(password),
                KeyDerivationPrf.HMACSHA512,
                5000,
                64
            ));
        }
    }
}
