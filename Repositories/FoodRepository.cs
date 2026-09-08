using Microsoft.EntityFrameworkCore;
using Naringskollen.Data;
using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Dtos.FoodMeasurementsDtos.Out;
using Naringskollen.Models;
using Naringskollen.Repositories.IRepositories;

namespace Naringskollen.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly NaringskollenDbContext context;        

        public FoodRepository(NaringskollenDbContext _context)
        {
            context = _context;
        }

        public async Task<List<FoodSummaryDto>> GetAllAsync(string query)
        {
            var foodSummaries = await context.Foods
                .AsNoTracking()
                .Where(f => f.Name.Contains(query))
                .Include(f => f.FoodMeasurements)
                .Include(f => f.Category)
                .Select(f => new FoodSummaryDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsSystem = f.IsSystem,
                    Category = f.Category.Name,
                    FoodMeasurements = f.FoodMeasurements
                        .Select(fm => new FoodMeasurementSummaryDto
                        {
                            Unit = fm.UnitName,
                            Grams = fm.GramWeight
                        })
                        .ToList()
                })
                .ToListAsync();
            return foodSummaries;
        }

        public async Task<Food?> GetByIdAsync(int id)
        {
            var foodDetail = await context.Foods
                .Include(f => f.FoodMeasurements)
                .Include(f => f.Category)
                .FirstOrDefaultAsync(f => f.Id == id);                

            return foodDetail;
        }

        public async Task<Food> CreateAsync(Food newFood)
        {
            context.Foods.Add(newFood);

            await context.SaveChangesAsync();

            return newFood;
        }

        public async Task<bool> UpdateAsync(Food updatedFood)
        {
            context.Foods.Update(updatedFood);

            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var rowsaffected = await context.Foods.Where(f => f.Id == id).ExecuteDeleteAsync();

            if (rowsaffected > 0)
            {
                return true;
            }

            return false;
        }

    }
}
