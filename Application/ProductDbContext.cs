using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>().HasKey(table => new
            {
                table.CategoryID,
                table.ProductID
            });
        }
    }
}
