using Naringskollen.Dtos.FoodMeasurementsDtos.In;
using Naringskollen.Models;

namespace Naringskollen.Services.IServices
{
    public interface IFoodMeasurementService
    {
        Task UpdateMeasurementsForFoodAsync(Food updateFood, List<UpdateFoodMeasurementDto> dtos);
    }
}