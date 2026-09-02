using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Naringskollen.Models;

namespace Naringskollen.Data
{
    public class NaringskollenDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public NaringskollenDbContext(DbContextOptions<NaringskollenDbContext> options) : base(options)
        {

        }

        public DbSet<FoodMeasurement> FoodMeasurements { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Grönsaker, potatis & rotfrukter"
                },
                new Category
                {
                    Id = 2,
                    Name = "Frukt & bär"
                },
                new Category
                {
                    Id = 3,
                    Name = "Bröd, pasta & gryn"
                },
                new Category
                {
                    Id = 4,
                    Name = "Nötter, frön & baljväxter"
                },
                new Category
                {
                    Id = 5,
                    Name = "Kött, fågel & chark"
                },
                new Category
                {
                    Id = 6,
                    Name = "Fisk & skaldjur"
                },
                new Category
                {
                    Id = 7,
                    Name = "Ägg, mejeri & växtbaserat"
                },
                new Category
                {
                    Id = 8,
                    Name = "Fetter, oljor & såser"
                },
                new Category
                {
                    Id = 9,
                    Name = "Färdiga rätter & snabbmat"
                },
                new Category
                {
                    Id = 10,
                    Name = "Sötsaker, snacks & bakverk"
                },
                new Category
                {
                    Id = 11,
                    Name = "Drycker"
                },
                new Category
                {
                    Id = 12,
                    Name = "Skafferi & kryddor"
                }
            );
        }
    }
}
