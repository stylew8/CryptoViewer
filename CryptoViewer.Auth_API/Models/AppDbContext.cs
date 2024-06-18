using System.Collections.Generic;
using CryptoViewer.API.Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoViewer.Auth_API.Models
{
    /// <summary>
    /// Represents the database context for the CryptoViewer application.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet representing local users in the database.
        /// </summary>
        public DbSet<LocalUser> LocalUsers { get; set; }
    }
}