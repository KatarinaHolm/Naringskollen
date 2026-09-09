using Naringskollen.Data;
using Naringskollen.Models;
using Naringskollen.Repositories.IRepositories;

namespace Naringskollen.Repositories
{
    public class FoodMeasurementRepository : IFoodMeasurementRepository
    {
        private readonly NaringskollenDbContext context;

        public FoodMeasurementRepository(NaringskollenDbContext _context)
        {
            context = _context;
        }

        public async Task<List<FoodMeasurement>> CreateAsync(List<FoodMeasurement> foodMeasurements)
        {
            foreach (var foodMeasurement in foodMeasurements)
            {
                context.Add(foodMeasurement);
            }

            await context.SaveChangesAsync();

            return foodMeasurements;
        }

        public async Task<bool> UpdateAsync(List<FoodMeasurement> foodMeasurements)
        {
            foreach (var foodMeasurement in foodMeasurements)
            {
                context.FoodMeasurements.Update(foodMeasurement);
            }
            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
