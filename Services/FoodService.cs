
using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodDtos.In;
using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Dtos.FoodMeasurementsDtos.Out;
using Naringskollen.Models;
using Naringskollen.Repositories.IRepositories;
using Naringskollen.Services.IServices;

namespace Naringskollen.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository foodRepository;
        private readonly ICategoriesRepository categoriesRepository;
        private readonly IFoodMeasurementService foodMeasurementService;
        private readonly IFoodMeasurementRepository foodMeasurementRepository;

        public FoodService(IFoodRepository _foodRepository, ICategoriesRepository _categoriesRepository, IFoodMeasurementService _foodMeasurementService, IFoodMeasurementRepository _foodMeasurementRepository)
        {
            foodRepository = _foodRepository;
            categoriesRepository = _categoriesRepository;
            foodMeasurementService = _foodMeasurementService;
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

            return MapToFoodDetailDto(foodDetail, foodDetail.Category.Name);
        }

        //For Users
        public async Task<CalculatedNutritionDto> GetCalculatedNutritionByIdAsync(int id, decimal quantity, string unit)
        {
            var foodDetail = await foodRepository.GetByIdAsync(id);

            if (foodDetail == null)
            {
                throw new KeyNotFoundException("Livsmedel kunde inte hittas");
            }

            if (unit is not ("g" or "kg") && !foodDetail.FoodMeasurements.Any(fm => fm.UnitName == unit))
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

            if (dto.FoodMeasurements.Any())
            {
                if (dto.FoodMeasurements.Any(fm => fm == null || fm.Unit == null || fm.Grams == null))
                {
                    throw new ArgumentException("Enhet får inte innehålla null-värden.");
                }

                var foodMeasurements = dto.FoodMeasurements
                    .Select(fm => new FoodMeasurement
                    {
                        UnitName = fm.Unit!.ToString(),
                        GramWeight = fm.Grams!.Value,
                        FoodId = savedNewFood.Id
                    })
                    .ToList();

                var savedFoodMeasurements = await foodMeasurementRepository.CreateAsync(foodMeasurements);

                savedNewFood.FoodMeasurements = savedFoodMeasurements;
            }

            return MapToFoodDetailDto(savedNewFood, category.Name);
        }

        public async Task<FoodDetailDto> UpdateAsync(int id, UpdateFoodDto dto)
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

            //Updating props
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

            // Checking catergory exists
            var category = await categoriesRepository.GetById(dto.CategoryId);

            if (category == null)
            {
                throw new KeyNotFoundException(
                    $"Kategori med id {dto.CategoryId} kunde inte hittas.");
            }

            // Updating foodmeasurments
            await foodMeasurementService.UpdateMeasurementsForFoodAsync(updateFood, dto.FoodMeasurements);

            var isUpdated = await foodRepository.UpdateAsync(updateFood);

            if (!isUpdated)
            {
                throw new DbUpdateException("Inga ändringar sparades i databasen.");
            }

            return MapToFoodDetailDto(updateFood, category.Name);

        }

        public async Task<FoodDetailDto> UpdateFoodMetadataAsync(int id, UpdateFoodMetadataDto dto)
        {
            var updateFood = await foodRepository.GetByIdAsync(id);

            if (updateFood == null)
            {
                throw new KeyNotFoundException("Livsmedel kunde inte hittas");
            }

            updateFood.Oxalate = dto.Oxalate;
            updateFood.CategoryId = dto.CategoryId;

            var category = await categoriesRepository.GetById(dto.CategoryId);

            // Checking catergory exists
            if (category == null)
            {
                throw new KeyNotFoundException(
                    $"Kategori med id {dto.CategoryId} kunde inte hittas.");
            }

            // Updating foodmeasurments
            await foodMeasurementService.UpdateMeasurementsForFoodAsync(updateFood, dto.FoodMeasurements);

            var isUpdated = await foodRepository.UpdateAsync(updateFood);

            if (!isUpdated)
            {
                throw new DbUpdateException("Inga ändringar sparades i databasen.");
            }

            return MapToFoodDetailDto(updateFood, category.Name);

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

        private static FoodDetailDto MapToFoodDetailDto(Food food, string categoryName)
        {
            return new FoodDetailDto
            {
                Id = food.Id,
                ExternalId = food.ExternalId,
                Name = food.Name,
                IsSystem = food.IsSystem,
                Oxalate = food.Oxalate,
                Kcal = food.Kcal,
                Fat = food.Fat,
                Protein = food.Protein,
                Carbohydrate = food.Carbohydrate,
                Fiber = food.Fiber,
                TotalSugar = food.TotalSugar,
                SaturatedFat = food.SaturatedFat,
                MonounsaturatedFat = food.MonounsaturatedFat,
                PolyunsaturatedFat = food.PolyunsaturatedFat,
                CategoryId = food.CategoryId,
                Category = categoryName,

                FoodMeasurements = food.FoodMeasurements
                    .Select(fm => new FoodMeasurementSummaryDto
                    {
                        Id = fm.Id,
                        Unit = fm.UnitName,
                        Grams = fm.GramWeight
                    })
                    .ToList()
            };
        }
    }
}
