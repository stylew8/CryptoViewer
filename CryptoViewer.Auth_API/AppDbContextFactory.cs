using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoViewer.Auth_API
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).Build();

            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

            var connectionString = config.GetValue<string>("ConnectionStrings:Authorization");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySQL(connectionString ?? "");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
