using Naringskollen.Dtos.CategoriesDtos.Out;

namespace Naringskollen.Services.IServices
{
    public interface ICategoriesService
    {
        Task<List<CategoriesSummaryDto>> GetAll();
        Task<CategoriesSummaryDto> GetById(int id);
    }
}