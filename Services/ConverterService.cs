using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Models;
using Naringskollen.Models.Enums;

namespace Naringskollen.Services
{
    public static class ConverterService
    {
        public static CalculatedNutritionDto CalculateNutrition(Food food, decimal quantity, FoodMeasurementUnit unit)
        {   
            //Calculate weight of requested amount of food
            decimal weightInGrams = unit switch
            {
                FoodMeasurementUnit.g => quantity,
                FoodMeasurementUnit.kg => quantity * 1000,
                FoodMeasurementUnit.styck or FoodMeasurementUnit.skiva => CalculateByPiece(food.FoodMeasurements, quantity, unit),
                FoodMeasurementUnit.dl => CalculateByDl(food.FoodMeasurements, quantity, unit),
                FoodMeasurementUnit.msk => 0.15m * CalculateByDl(food.FoodMeasurements, quantity, unit),
                FoodMeasurementUnit.tsk => 0.05m * CalculateByDl(food.FoodMeasurements, quantity, unit),
                _ => quantity
            };

            decimal CalculateByPiece(List<FoodMeasurement> foodMeasurements, decimal quantity, FoodMeasurementUnit unit)
            {
                var foodMeasurement = foodMeasurements.FirstOrDefault(fm => fm.Unit == unit);
                return quantity * foodMeasurement.GramWeight;
            };

            decimal CalculateByDl(List<FoodMeasurement> foodMeasurements, decimal quantity, FoodMeasurementUnit unit)
            {
                var foodMeasurementByDl = foodMeasurements.FirstOrDefault(fm => fm.Unit == FoodMeasurementUnit.dl);
                return quantity * foodMeasurementByDl.GramWeight;
            }; 
            
            // Calculate the nutrient of the food and mapping in to 
            decimal multiplier = weightInGrams / 100m;

            decimal CalculateNutrient(decimal? baseValue)
            {
                return Math.Round((baseValue ?? 0m) * multiplier, 2, MidpointRounding.AwayFromZero);
            }

            var dto = new CalculatedNutritionDto
            {
                Name = food.Name,

                Quantity = quantity,

                Unit = unit,

                CalculatedGrams = weightInGrams,

                Oxalate = CalculateNutrient(food.Oxalate),

                Kcal = CalculateNutrient(food.Kcal),

                Fat = CalculateNutrient(food.Fat),

                Protein = CalculateNutrient(food.Protein),

                Carbohydrate = CalculateNutrient(food.Carbohydrate),

                Fiber = CalculateNutrient(food.Fiber),

                TotalSugar = CalculateNutrient(food.TotalSugar),

                SaturatedFat = CalculateNutrient(food.SaturatedFat),

                MonounsaturatedFat = CalculateNutrient(food.MonounsaturatedFat),

                PolyunsaturatedFat = CalculateNutrient(food.PolyunsaturatedFat),

                Category = food.Category.Name
            };

            return dto;
        }        
    }
}
