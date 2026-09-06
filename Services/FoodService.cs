using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodDtos.In;
using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Dtos.FoodMeasurementsDtos.Out;
using Naringskollen.Models;
using Naringskollen.Repositories.IRepositories;

namespace Naringskollen.Services
{
    public class FoodService
    {
        private readonly IFoodRepository foodRepository;

        //Create, Put, Patch och Delete.

        //Obs! isSystem = true means the food is not from Livsmedelverkets database.

        //GetAll() - SummaryDto

        //GetalculatedNutritionById - no dto.
        //Id in Route, others in query: [FromQuery] decimal quantity, [FromQuery] string unit

        public FoodService(IFoodRepository _foodRepository)
        {
            foodRepository = _foodRepository;
        }

        public async Task<List<FoodSummaryDto>> GetAllAsync(string query)
        {
            var foodSummaries = await foodRepository.GetAllAsync(query);
                
            return foodSummaries;
        }

        // For Admin
        public async Task<FoodDetailDto?> GetByIdAsync(int id)
        {
            var foodDetail = await foodRepository.GetByIdAsync(id);

            //Check null

            var foodDto = new FoodDetailDto
            {
                Id = foodDetail.Id,

                ExternalId = foodDetail.ExternalId,

                Name = foodDetail.Name,

                IsSystem = foodDetail.IsSystem,

                Oxalate = foodDetail.Oxalate,

                Kcal = foodDetail.Kcal,

                Fat = foodDetail.Fat,

                Protein = foodDetail.Protein,

                Carbohydrate = foodDetail.Carbohydrate,

                Fiber = foodDetail.Fiber,

                TotalSugar = foodDetail.TotalSugar,

                SaturatedFat = foodDetail.SaturatedFat,

                MonounsaturatedFat = foodDetail.MonounsaturatedFat,

                PolyunsaturatedFat = foodDetail.PolyunsaturatedFat,

                CategoryId = foodDetail.CategoryId,

                Category = foodDetail.Category.Name,

                FoodMeasurements = foodDetail.FoodMeasurements
                        .Select(fm => new FoodMeasurementSummaryDto
                        {
                            Unit = fm.UnitName,
                            Grams = fm.GramWeight
                        })
                        .ToList()
            };

            return foodDto;
        }

        //For Users
        public async Task<CalculatedNutritionDto> GetCalculatedNutritionByIdAsync(int id, decimal quantity, string unit)
        {
            var foodDetail = await foodRepository.GetByIdAsync(id);

            //Check null

            var calculatedNutrition = ConverterService.CalculateNutrition(foodDetail, quantity, unit);

            return calculatedNutrition;
        }

        public async Task<FoodDetailDto> CreateAsync(CreateFoodDto dto)
        {
            //validation of user input

            var newFood = new Food
            {
                Name = dto.Name,

                IsSystem = true,

                Oxalate = dto.Oxalate,

                Kcal = dto.Kcal,

                Fat = dto.Fat,

                Protein = dto.Protein,

                Carbohydrate = dto.Carbohydrate,

                Fiber = dto.Fiber,

                TotalSugar = dto.TotalSugar,

                SaturatedFat = dto.SaturatedFat,

                MonounsaturatedFat = dto.MonounsaturatedFat,

                PolyunsaturatedFat = dto.PolyunsaturatedFat,

                CategoryId = dto.CategoryId,

                FoodMeasurements = dto.FoodMeasurements
                        .Select(fm => new FoodMeasurement
                        {
                            UnitName = fm.Unit,
                            GramWeight = fm.Grams
                        })
                        .ToList()

            };

            var savedNewFood = await foodRepository.CreateAsync(newFood);
            //SKicka tillbaka objekt eller endast bekräftelse?

            var foodDto = new FoodDetailDto
            {
                Id = savedNewFood.Id,                

                Name = savedNewFood.Name,

                IsSystem = savedNewFood.IsSystem,

                Oxalate = savedNewFood.Oxalate,

                Kcal = savedNewFood.Kcal,

                Fat = savedNewFood.Fat,

                Protein = savedNewFood.Protein,

                Carbohydrate = savedNewFood.Carbohydrate,

                Fiber = savedNewFood.Fiber,

                TotalSugar = savedNewFood.TotalSugar,

                SaturatedFat = savedNewFood.SaturatedFat,

                MonounsaturatedFat = savedNewFood.MonounsaturatedFat,

                PolyunsaturatedFat = savedNewFood.PolyunsaturatedFat,

                CategoryId = savedNewFood.CategoryId,

                Category = savedNewFood.Category.Name,

                FoodMeasurements = savedNewFood.FoodMeasurements
                       .Select(fm => new FoodMeasurementSummaryDto
                       {
                           Unit = fm.UnitName,
                           Grams = fm.GramWeight
                       })
                       .ToList()
            };
            return foodDto;
        }

        public async Task<bool> UpdateAsync(int id, UpdateFoodDto dto)
        {
            var updateFood = await foodRepository.GetByIdAsync(id);
            //Check null

            updateFood.Name = dto.Name;

            updateFood.Oxalate = dto.Oxalate;

            updateFood.Kcal = dto.Kcal;

            updateFood.Fat = dto.Fat;

            updateFood.Protein = dto.Protein;

            updateFood.Carbohydrate = dto.Carbohydrate;

            updateFood.Fiber = dto.Fiber;

            updateFood.TotalSugar = dto.TotalSugar;

            updateFood.SaturatedFat = dto.SaturatedFat;

            updateFood.MonounsaturatedFat = dto.MonounsaturatedFat;

            updateFood.PolyunsaturatedFat = dto.PolyunsaturatedFat;

            updateFood.CategoryId = dto.CategoryId;

            updateFood.FoodMeasurements = dto.FoodMeasurements
                .Select(fm =>
                    new FoodMeasurement
                    {
                        UnitName = fm.Unit,
                        GramWeight = fm.Grams
                    })
                .ToList();


            var isUpdated = await foodRepository.UpdateAsync(updateFood);
            return isUpdated;
        }

        public async Task<bool> UpdateFoodMetadataAsync(int id, UpdateFoodMetadataDto dto)
        {
            var updateFood = await foodRepository.GetByIdAsync(id);
            //Check null

            updateFood.Name = dto.Name;

            updateFood.Oxalate = dto.Oxalate;

            updateFood.CategoryId = dto.CategoryId;

            updateFood.FoodMeasurements = dto.FoodMeasurements
                .Select(fm =>
                    new FoodMeasurement
                    {
                        UnitName = fm.Unit,
                        GramWeight = fm.Grams
                    })
                .ToList();


            var isUpdated = await foodRepository.UpdateAsync(updateFood);
            return isUpdated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDeleted = await foodRepository.DeleteAsync(id);

            return isDeleted;
        }
    }
}
