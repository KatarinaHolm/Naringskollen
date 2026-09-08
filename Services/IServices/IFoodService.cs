using Naringskollen.Dtos.FoodDtos.In;
using Naringskollen.Dtos.FoodDtos.Out;

namespace Naringskollen.Services.IServices
{
    public interface IFoodService
    {
        Task<FoodDetailDto> CreateAsync(CreateFoodDto dto);
        Task DeleteAsync(int id);
        Task<List<FoodSummaryDto>> GetAllAsync(string query);
        Task<FoodDetailDto> GetByIdAsync(int id);
        Task<CalculatedNutritionDto> GetCalculatedNutritionByIdAsync(int id, decimal quantity, string unit);
        Task UpdateAsync(int id, UpdateFoodDto dto);
        Task UpdateFoodMetadataAsync(int id, UpdateFoodMetadataDto dto);
    }
}