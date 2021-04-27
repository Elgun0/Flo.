using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace DataAccess
{
    public class EcommerceContext:IdentityDbContext<AppUser>
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Picture> Pictures{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
