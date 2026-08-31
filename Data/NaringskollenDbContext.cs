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
        }
    }
}
