using System.Reflection;
using FiorelloAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Social> Socials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Expert>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);

            modelBuilder.Entity<Setting>().HasData(
                new Setting
                {
                    Id = 1,
                    Key = "HeaderLogo",
                    Value = "logo.png",
                    SoftDeleted = false,
                    CreatedDate = DateTime.Now
                },
                new Setting
                {
                    Id = 2,
                    Key = "Phone",
                    Value = "234342334",
                    SoftDeleted = false,
                    CreatedDate = DateTime.Now
                },
                new Setting
                {
                    Id = 3,
                    Key = "Address",
                    Value = "Ehmedli",
                    SoftDeleted = false,
                    CreatedDate = DateTime.Now
                }
            );

             modelBuilder.Entity<Social>().HasData(
                new Social
                {
                    Id = 1,
                    Name = "Twitter",
                    Icon = "logo.png",
                    URL = "https://x.com/",
                    SoftDeleted = false,
                    CreatedDate = DateTime.Now
                },
                new Social
                {
                    Id = 2,
                    Name = "Instagram",
                    Icon = "logo.png",
                    URL = "https://www.instagram.com/",
                    SoftDeleted = false,
                    CreatedDate = DateTime.Now
                },
                new Social
                {
                    Id = 3,
                    Name = "Tumblr",
                    Icon = "logo.png",
                    URL = "https://www.tumblr.com/",
                    SoftDeleted = false,
                    CreatedDate = DateTime.Now
                },
                new Social
                {
                    Id = 4,
                    Name = "Pinterest",
                    Icon = "logo.png",
                    URL = "https://www.pinterest.com/",
                    SoftDeleted = false,
                    CreatedDate = DateTime.Now
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
