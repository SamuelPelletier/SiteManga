using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteManga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MangaOrder> MangaOrders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one
            const string ROLE_ID = ADMIN_ID;
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "admin"
            });

            string email = "admin@admin.com";
            var hasher = new PasswordHasher<User>();
            User user = new User
            {
                Id = ADMIN_ID,
                UserName = email,
                NormalizedUserName = email,
                Email = email,
                NormalizedEmail = email,
                EmailConfirmed = true,
                SecurityStamp = string.Empty
            };
            user.PasswordHash = hasher.HashPassword(user, "Password1@");

            modelBuilder.Entity<User>().HasData(user); 

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        public DbSet<SiteManga.Models.Address> Address { get; set; }

        public DbSet<SiteManga.Models.Comment> Comment { get; set; }
    }
}
