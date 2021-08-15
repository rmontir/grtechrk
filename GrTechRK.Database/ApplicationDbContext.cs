using GrTechRK.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics.CodeAnalysis;

namespace GrTechRK.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #region DbSet

        [NotNull]
        public DbSet<Company> Companies { get; set; }

        [NotNull]
        public DbSet<Employee> Employees { get; set; }

        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            UriToStringConverter uriToStringConverter = new UriToStringConverter();

            builder.Entity<Company>()
                .Property(c => c.Website)
                .HasConversion(uriToStringConverter);

            builder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<Employee>()
                .HasIndex(e => new { e.FirstName, e.LastName })
                .IsUnique();

            #region Seeds

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole("Admin") { NormalizedName = "ADMIN", Id = "31598018-8115-4cca-95d0-be83c7aabb00", ConcurrencyStamp = "d38eda93-4332-4f69-819a-e91cddf421f2" },
                    new IdentityRole("User") { NormalizedName = "USER", Id = "4a1b4ca5-7db5-4706-9214-b7fc01b73f68", ConcurrencyStamp = "a66d419b-40df-4851-9179-ab75df744b6c" }
                );

            builder.Entity<IdentityUser>()
                .HasData(
                    new IdentityUser
                    {
                        Id = "38e4c6d8-a44b-4b63-829a-d5f307c487a0",
                        UserName = "admin@grtech.com.my",
                        NormalizedUserName = "admin@grtech.com.my".ToUpper(),
                        Email = "admin@grtech.com.my",
                        NormalizedEmail = "admin@grtech.com.my".ToUpper(),
                        EmailConfirmed = true,
                        PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "password")
                    },
                    new IdentityUser
                    {
                        Id = "e082f561-367e-47f6-8724-371d7ad3d1d7",
                        UserName = "user@grtech.com.my",
                        NormalizedUserName = "user@grtech.com.my".ToUpper(),
                        Email = "user@grtech.com.my",
                        NormalizedEmail = "user@grtech.com.my".ToUpper(),
                        EmailConfirmed = true,
                        PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "password")
                    }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>
                    {
                        UserId = "38e4c6d8-a44b-4b63-829a-d5f307c487a0",
                        RoleId = "31598018-8115-4cca-95d0-be83c7aabb00"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "e082f561-367e-47f6-8724-371d7ad3d1d7",
                        RoleId = "4a1b4ca5-7db5-4706-9214-b7fc01b73f68"
                    }
                );

            #endregion
        }
    }
}
