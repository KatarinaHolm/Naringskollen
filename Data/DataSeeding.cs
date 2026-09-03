using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Naringskollen.Models;
using System.Text.Json;

namespace Naringskollen.Data
{
    public static class DataSeeding
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;


            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser<int>>>();
            var context = services.GetRequiredService<NaringskollenDbContext>();
            string adminPassword = app.Configuration["SeedAdmin:Password"];

            await context.Database.MigrateAsync();

            await SeedRolesAsync(roleManager);

            await SeedAdminAsync(userManager, adminPassword);

            await SeedFoodAsync(context);

            await SeedFoodMeasurmentAsync(context);
        }


        public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roles = { "Admin", "User" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = roleName });
                }
            }
        }

        public static async Task SeedAdminAsync(UserManager<IdentityUser<int>> userManager, string adminPassword)
        {
            string adminEmail = "katarinasofiaholm@gmail.com";
            string adminRole = "Admin";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new IdentityUser<int>
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Kunde inte skapa admin: {errors}");
                }
            }

            if (!await userManager.IsInRoleAsync(admin, adminRole))
            {
                var roleResult = await userManager.AddToRoleAsync(admin, adminRole);

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    throw new Exception($"Kunde inte koppla Admin-rollen: {errors}");
                }

            }

        }

        public static async Task SeedFoodAsync(NaringskollenDbContext context)
        {
            if (!context.Foods.Any())
            {
                var data = await File.ReadAllTextAsync("Data/SeedData/Food_data.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var foodItems = JsonSerializer.Deserialize<List<Food>>(data, options);

                if (foodItems != null)
                {
                    await context.Foods.AddRangeAsync(foodItems);

                    // Kolla upp

                    //using var transaction = await context.Database.BeginTransactionAsync();

                    //await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Foods ON");
                    //await context.SaveChangesAsync();
                    //await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Foods OFF");

                    //await transaction.CommitAsync();
                }
            }
        }

        public static async Task SeedFoodMeasurmentAsync(NaringskollenDbContext context)
        {
            if (!context.FoodMeasurements.Any())
            {
                var data = await File.ReadAllTextAsync("Data/SeedData/FoodMeasurment_data.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var foodMeasurements = JsonSerializer.Deserialize<List<FoodMeasurement>>(data, options);

                if (foodMeasurements != null)
                {
                    await context.FoodMeasurements.AddRangeAsync(foodMeasurements);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
