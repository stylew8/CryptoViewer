using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.API.Authorization.Models;

namespace CryptoViewer.Auth_API.Models.Dto
{
    public class LoginResponseDto
    {
        public LocalUser? User { get;set; }
        public string Token { get; set; }
    }
}
