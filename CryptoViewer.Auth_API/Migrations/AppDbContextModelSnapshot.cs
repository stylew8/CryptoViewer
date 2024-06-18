using CryptoViewer.API.Authorization.Models;
using CryptoViewer.Auth_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoViewer.Auth_API.Migrations
{
    /// <summary>
    /// Represents the model snapshot for the application's database context.
    /// </summary>
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        /// <summary>
        /// Builds the database model snapshot.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to construct the database model.</param>
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity<LocalUser>(b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.HasKey("Id");

                b.ToTable("LocalUsers");
            });
#pragma warning restore 612, 618
        }
    }
}