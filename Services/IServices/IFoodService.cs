using Naringskollen.Dtos.FoodDtos.In;
using Naringskollen.Dtos.FoodDtos.Out;

namespace Naringskollen.Services.IServices
{
    public interface IFoodService
    {
        Task<FoodDetailDto> CreateAsync(CreateFoodDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<FoodSummaryDto>> GetAllAsync(string query);
        Task<FoodDetailDto> GetByIdAsync(int id);
        Task<CalculatedNutritionDto> GetCalculatedNutritionByIdAsync(int id, decimal quantity, string unit);
        Task<bool> UpdateAsync(int id, UpdateFoodDto dto);
        Task<bool> UpdateFoodMetadataAsync(int id, UpdateFoodMetadataDto dto);
    }
}