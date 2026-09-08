using Naringskollen.Dtos.FoodDtos.Out;
using Naringskollen.Models;

namespace Naringskollen.Services
{
    public static class ConverterService
    {
        public static CalculatedNutritionDto CalculateNutrition(Food food, decimal quantity, string unit)
        {   
            //Calculate weight of requested amount of food
            decimal weightInGrams = unit switch
            {
                "g" => quantity,
                "kg" => quantity * 1000,
                "styck" or "skiva" => CalculateByPiece(food.FoodMeasurements, quantity, unit),
                "dl" => CalculateByDl(food.FoodMeasurements, quantity, unit),
                "msk" => 0.15m * CalculateByDl(food.FoodMeasurements, quantity, unit),
                "tsk" => 0.05m * CalculateByDl(food.FoodMeasurements, quantity, unit),
                _ => quantity
            };

            decimal CalculateByPiece(List<FoodMeasurement> foodMeasurements, decimal quantity, string unit)
            {
                var foodMeasurement = foodMeasurements.FirstOrDefault(fm => fm.UnitName == unit);
                return quantity * foodMeasurement.GramWeight;
            };

            decimal CalculateByDl(List<FoodMeasurement> foodMeasurements, decimal quantity, string unit)
            {
                var foodMeasurementByDl = foodMeasurements.FirstOrDefault(fm => fm.UnitName == "dl");
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
