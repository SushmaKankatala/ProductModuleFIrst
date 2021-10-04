using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductFirstModule.Database.Entities;

namespace ProductFirstModule.Database
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Product_s { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>()
                .HasKey(o => new { o.ProductId, o.UserId });
        }
    }
}
