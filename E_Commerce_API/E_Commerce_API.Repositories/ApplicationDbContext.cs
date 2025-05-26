using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace Product.DataAccess
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)    {
        
        public DbSet<Category> Categories { get; set; }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Groceries",
                    CategoryDescription = "Grocery Items",
                    IsDelete = 0,
                    CreatedAt = new DateTime(2024, 01, 01),
                    UpdatedAt = new DateTime(2024, 01, 01)
                },
                new Category
                 {
                     Id = 2,
                     CategoryName = "Electronics",
                     CategoryDescription = "Electronic Items",
                     IsDelete = 0,
                     CreatedAt = new DateTime(2024, 01, 01),
                     UpdatedAt = new DateTime(2024, 01, 01)
                }
            );

            modelBuilder.Entity<Products>().HasData(
                new Products
                {
                    Id = 1,
                    ProductName = "Sugar",
                    ProductDescription = "To make tasty",
                    CategoryId = 1,
                    IsDelete = 0,
                    CreatedAt = new DateTime(2024, 01, 01),
                    UpdatedAt = new DateTime(2024, 01, 01)
                },
                new Products
                {
                    Id = 2,
                    ProductName = "Sony",
                    ProductDescription = "Sony Television",
                    CategoryId =2,
                    IsDelete = 0,
                    CreatedAt = new DateTime(2024, 01, 01),
                    UpdatedAt = new DateTime(2024, 01, 01)
                }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
