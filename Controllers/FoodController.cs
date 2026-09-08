using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Naringskollen.Dtos.FoodDtos.In;
using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Services.IServices;

namespace Naringskollen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService foodService;

        public FoodController(IFoodService _foodService)
        {
            foodService = _foodService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FoodSummaryDto>>> GetAll([FromQuery] string query)
        {
            var queryFoodSummaries = await foodService.GetAllAsync(query);

            return Ok(queryFoodSummaries);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<FoodSummaryDto>>> GetById([FromRoute] int id)
        {
            var food = await foodService.GetByIdAsync(id);

            return Ok(food);
        }

        
        [HttpGet("{id:int}/calculate")]
        public async Task<ActionResult<CalculatedNutritionDto>> GetCalculatedNutrition([FromRoute] int id, [FromQuery] decimal quantity, [FromQuery] string unit)
        {
            var food = await foodService.GetCalculatedNutritionByIdAsync(id, quantity, unit);

            return Ok(food);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]       
        public async Task<ActionResult<FoodDetailDto>> Create([FromBody] CreateFoodDto dto)
        {
            var createdFood = await foodService.CreateAsync(dto);
            return Created("api/foodcontroller", createdFood);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]UpdateFoodDto dto)
        {
            await foodService.UpdateAsync(id, dto);        

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateFoodMetadata([FromRoute] int id, [FromBody] UpdateFoodMetadataDto dto)
        {
            await foodService.UpdateFoodMetadataAsync(id, dto);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await foodService.DeleteAsync(id);

            return NoContent();
        }

        //All CRUDs
        //Authorize: Admin -  on Create, Put, Patch och Delete.

        //Obs! isSystem = true means the food is not from Livsmedelverkets database.

        //GetAll() - SummaryDto

        //GetById - no dto.
        //Id in Route, others in query: [FromQuery] decimal quantity, [FromQuery] string unit
    }
}
