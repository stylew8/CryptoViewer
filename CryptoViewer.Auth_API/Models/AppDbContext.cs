
using System.Collections.Generic;
using CryptoViewer.API.Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoViewer.Auth_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<LocalUser> LocalUsers { get; set; }
    }
}
