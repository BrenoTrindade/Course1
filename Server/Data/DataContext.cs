using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;

namespace Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Categories seed

            Category[] categoriesToSeed = new Category[3];

            for (int i = 1; i < 4; i++)
            {
                categoriesToSeed[i - 1] = new Category
                {
                    categoryId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Name = $"Category {i}",
                    Description = $"A description of category {i}"
                };
            }

            modelBuilder.Entity<Category>().HasData(categoriesToSeed);
            #endregion

            modelBuilder.Entity<Post>(
                entity =>
                {
                    entity.HasOne(post => post.Category)
                    .WithMany(category => category.Posts)
                    .HasForeignKey("categoryId");
                });

            #region Posts seed

            Post[] postsToSeed = new Post[6];

            for (int i = 1; i < 7; i++)
            {
                string postTitle = string.Empty;
                int categoryId = 0;

                switch (i) 
                {
                    case 1:
                        postTitle = "First post";
                        categoryId = 1;
                        break;
                    case 2:
                        postTitle = "Second post";
                        categoryId = 2;
                        break;
                    case 3:
                        postTitle = "Third post";
                        categoryId = 3;
                        break;
                    case 4:
                        postTitle = "Fourth post";
                        categoryId = 1;
                        break;
                    case 5:
                        postTitle = "Fifith post";
                        categoryId = 2;
                        break;
                    case 6:
                        postTitle = "Sixth post";
                        categoryId = 3;
                        break;
                    default:
                        break;

                }

                postsToSeed[i - 1] = new Post 
                { 
                    PostId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Title = postTitle,
                    Excerpt = $"This is the excerpt for opst {i}. An excerpt is a little extration from a larger piece of text. Sort of like a preview.",
                    Content = string.Empty,
                    PublishDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm"),
                    Published = true,
                    Author = "Breno Trindade",
                    categoryId = categoryId
                };
            }

            modelBuilder.Entity<Post>().HasData(postsToSeed);
            #endregion
        }
    }
}
