using Naringskollen.Models;

namespace Naringskollen.Repositories
{
    public interface IFoodMeasurementRepository
    {
        Task<List<FoodMeasurement>> CreateAsync(List<FoodMeasurement> foodMeasurements);
    }
}