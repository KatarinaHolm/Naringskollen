
using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodDtos.In;
using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Dtos.FoodMeasurementsDtos.Out;
using Naringskollen.Models;
using Naringskollen.Repositories;
using Naringskollen.Repositories.IRepositories;
using Naringskollen.Services.IServices;

namespace Naringskollen.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository foodRepository;
        private readonly ICategoriesRepository categoriesRepository;
        private readonly IFoodMeasurementRepository foodMeasurementRepository;

        public FoodService(IFoodRepository _foodRepository, ICategoriesRepository _categoriesRepository, IFoodMeasurementRepository _foodMeasurementRepository)
        {
            foodRepository = _foodRepository;
            categoriesRepository = _categoriesRepository;
            foodMeasurementRepository = _foodMeasurementRepository;
        }

        public async Task<List<FoodSummaryDto>> GetAllAsync(string query)
        {
            var foodSummaries = await foodRepository.GetAllAsync(query);

            return foodSummaries;
        }

        // For Admin
        public async Task<FoodDetailDto> GetByIdAsync(int id)
        {
            var foodDetail = await foodRepository.GetByIdAsync(id);

            if (foodDetail == null)
            {
                throw new KeyNotFoundException("Livsmedel kunde inte hittas");
            }

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

            if (foodDetail == null)
            {
                throw new KeyNotFoundException("Livsmedel kunde inte hittas");
            }
            if (unit is not ("g" or "kg") &&
                !foodDetail.FoodMeasurements.Any(fm => fm.UnitName == unit))
            {
                throw new ArgumentException("Enhet är inte giltig för livsmedlet");
            }

            var calculatedNutrition = ConverterService.CalculateNutrition(foodDetail, quantity, unit);

            return calculatedNutrition;
        }

        public async Task<FoodDetailDto> CreateAsync(CreateFoodDto dto)
        {            
            var category = await categoriesRepository.GetById(dto.CategoryId);

            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori med id {dto.CategoryId} kunde inte hittas.");
            }

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

                
            };
            

            var savedNewFood = await foodRepository.CreateAsync(newFood);
            var foodMeasurementSummaries = new List<FoodMeasurementSummaryDto>();

            if (dto.FoodMeasurements.Any())
            {
                var foodMeasurements = dto.FoodMeasurements
                    .Select(fm => new FoodMeasurement
                    {
                        UnitName = fm.Unit,
                        GramWeight = fm.Grams,
                        FoodId = savedNewFood.Id
                    })
                    .ToList();

                var savedFoodMeasurements = await foodMeasurementRepository.CreateAsync(foodMeasurements);

                foodMeasurementSummaries = savedFoodMeasurements
                    .Select(fm => new FoodMeasurementSummaryDto
                    {
                        Unit = fm.UnitName,
                        Grams = fm.GramWeight
                    })
                    .ToList();
            }

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

                Category = category.Name,

                FoodMeasurements = foodMeasurementSummaries
            };
            return foodDto;
        }

        public async Task UpdateAsync(int id, UpdateFoodDto dto)
        {
            var updateFood = await foodRepository.GetByIdAsync(id);

            if (updateFood == null)
            {
                throw new KeyNotFoundException("Livsmedel kunde inte hittas");
            }
            else if (!updateFood.IsSystem)
            {
                throw new InvalidOperationException("Näringsinnehåll för livsmedel från Livsmedelsverket kan inte ändras. Använd uppdatering av metadata istället.");
            }

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

            if (!isUpdated)
            {
                throw new DbUpdateException("Inga ändringar sparades i databasen.");
            }
            
        }

        public async Task UpdateFoodMetadataAsync(int id, UpdateFoodMetadataDto dto)
        {
            var updateFood = await foodRepository.GetByIdAsync(id);

            if (updateFood == null)
            {
                throw new KeyNotFoundException("Livsmedel kunde inte hittas");
            }            

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

            if (!isUpdated)
            {
                throw new DbUpdateException("Inga ändringar sparades i databasen.");
            }

        }

        public async Task DeleteAsync(int id)
        {
            var deleteFood = await foodRepository.GetByIdAsync(id);

            if (deleteFood == null)
            {
                throw new KeyNotFoundException("Livsmedel kunde inte hittas.");
            }
            else if (!deleteFood.IsSystem)
            {
                throw new InvalidOperationException("Livsmedel från Livsmedelsverket kan inte raderas.");
            }

            var isDeleted = await foodRepository.DeleteAsync(id);

            if (!isDeleted)
            {
                throw new Exception("Kunde inte radera livsmedlet från databasen.");
            }
        }
    }
}
