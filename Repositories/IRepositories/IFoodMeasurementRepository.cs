using Naringskollen.Models;

namespace Naringskollen.Repositories.IRepositories
{
    public interface IFoodMeasurementRepository
    {
        Task<List<FoodMeasurement>> CreateAsync(List<FoodMeasurement> foodMeasurements);
        Task<bool> UpdateAsync(List<FoodMeasurement> foodMeasurements);
    }
}