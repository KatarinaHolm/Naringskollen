using Naringskollen.Dtos.FoodMeasurementsDtos.In;
using Naringskollen.Models;

namespace Naringskollen.Services.IServices
{
    public interface IFoodMeasurementService
    {
        void ValidateCreateMeasurements(List<CreateFoodMeasurementDto>? dtos);
        void ReplaceMeasurementsForFood(Food food, List<UpdateFoodMeasurementDto>? dtos);
    }
}
