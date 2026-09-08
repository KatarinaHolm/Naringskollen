using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.CategoriesDtos;
using Naringskollen.Dtos.CategoriesDtos.Out;
using Naringskollen.Models;
using Naringskollen.Repositories.IRepositories;
using Naringskollen.Services.IServices;

namespace Naringskollen.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository categoriesRepository;

        public CategoriesService(ICategoriesRepository _categoriesRepository)
        {
            categoriesRepository = _categoriesRepository;
        }

        public async Task<List<CategoriesSummaryDto>> GetAll()
        {
            var categories = await categoriesRepository.GetAll();

            var categoriesDtos = categories.Select(c => new CategoriesSummaryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return categoriesDtos;
        }

        public async Task<CategoriesSummaryDto> GetById(int id)
        {
            var category = await categoriesRepository.GetById(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori med id {id} kunde inte hittas.");
            }

            var categoriesDto = new CategoriesSummaryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return categoriesDto;
        }
    }
}
