using Naringskollen.Dtos.FoodMeasurementsDtos;

namespace Naringskollen.Dtos.FoodDtos.In
{
    public class UpdateFoodDto
    {
        public string Name { get; set; }

        public double? Oxalate { get; set; }

        public double Kcal { get; set; }

        public double Fat { get; set; }

        public double Protein { get; set; }

        public double Carbohydrate { get; set; }

        public double Fiber { get; set; }

        public double TotalSugar { get; set; }

        public double? SaturatedFat { get; set; }

        public double? MonounsaturatedFat { get; set; }

        public double? PolyunsaturatedFat { get; set; }

        public int CategoryId { get; set; }

        public List<FoodMeasurementDto> FoodMeasurements { get; set; }
    }
}
