using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Category[] categoriesToSeed = new Category[3];

            for (int i = 0; i < 3; i++)
            {
                categoriesToSeed[i] = new Category 
                { 
                    categoryId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Name = $"Category {i}",
                    Description = $"A description of category {i}"
                };
            }

            modelBuilder.Entity<Category>().HasData(categoriesToSeed);
        }
    }
}
