using Naringskollen.Dtos.FoodMeasurementsDtos;

namespace Naringskollen.Models
{
    public class Food
    {
        public int Id { get; set; }

        public int? ExternalId { get; set; }

        public string Name { get; set; }

        public bool IsSystem { get; set; }

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

        public Category Category { get; set; }

        public List<FoodMeasurement> FoodMeasurements { get; set; }
    }
}
