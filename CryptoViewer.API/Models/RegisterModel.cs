﻿namespace CryptoViewer.API.Models
{
    public class RegisterModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public string Address { get; set; }

        public string Username { get; set; }
        public string Password { get; set; } 
        public string Salt { get; set; }
    }
}
