using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Models;

namespace Naringskollen.Repositories.IRepositories
{
    public interface IFoodRepository
    {
        Task<Food> CreateAsync(Food newFood);
        Task<bool> DeleteAsync(int id);
        Task<List<FoodSummaryDto>> GetAllAsync(string query);
        Task<Food?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Food updatedFood);
    }
}