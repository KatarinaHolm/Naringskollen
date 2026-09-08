using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Naringskollen.Dtos.CategoriesDtos.Out;
using Naringskollen.Repositories;
using Naringskollen.Repositories.IRepositories;
using Naringskollen.Services.IServices;

namespace Naringskollen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService _categoriesService)
        {
            categoriesService = _categoriesService;
        }

        public async Task<ActionResult<List<CategoriesSummaryDto>>> GetAll()
        {
            var categories = await categoriesService.GetAll();
           
            return Ok(categories);
        }

        public async Task<ActionResult<CategoriesSummaryDto>> GetById(int id)
        {
            var category = await categoriesService.GetById(id);
           
            return Ok(category);
        }

    }
}
