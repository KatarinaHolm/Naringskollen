using Naringskollen.Data;
using Naringskollen.Models;

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
    }
}
