
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Naringskollen.Data;
using Scalar.AspNetCore;

namespace Naringskollen
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<NaringskollenDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentityApiEndpoints<IdentityUser<int>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<NaringskollenDbContext>();

            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            await app.InitializeDatabaseAsync();

            //Middleware?

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapIdentityApi<IdentityUser<int>>();

            app.MapControllers();

            app.Run();
        }
    }
}
