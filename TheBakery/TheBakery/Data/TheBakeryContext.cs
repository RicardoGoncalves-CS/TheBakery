using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBakery.Models;

namespace TheBakery.Data
{
    public class TheBakeryContext : DbContext
    {
        public TheBakeryContext (DbContextOptions<TheBakeryContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Address { get; set; } = default!;

        public DbSet<Customer> Customer { get; set; } = default!;

        public DbSet<Order> Order { get; set; } = default!;

        public DbSet<OrderDetails> OrderDetails { get; set; } = default!;

        public DbSet<Product> Product { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Address>()
                .HasKey(a => a.AddressId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address)
                .WithMany()
                .HasForeignKey(c => c.AddressId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
