using Naringskollen.Models.Enums;
using System.Text.Json.Serialization;

namespace Naringskollen.Dtos.FoodDtos.Out
{
    public class CalculatedNutritionDto
    {
        public string Name { get; set; }

        public decimal Quantity { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FoodMeasurementUnit? Unit { get; set; }

        public decimal CalculatedGrams { get; set; }     

        public decimal? Oxalate { get; set; }

        public decimal Kcal { get; set; }

        public decimal Fat { get; set; }

        public decimal Protein { get; set; }

        public decimal Carbohydrate { get; set; }

        public decimal Fiber { get; set; }

        public decimal TotalSugar { get; set; }

        public decimal? SaturatedFat { get; set; }

        public decimal? MonounsaturatedFat { get; set; }

        public decimal? PolyunsaturatedFat { get; set; }
                
        public string Category { get; set; } 

    }
}
