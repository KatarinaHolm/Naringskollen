using Naringskollen.Dtos.FoodMeasurementsDtos;
using Naringskollen.Models;

namespace Naringskollen.Dtos.FoodDtos.Out
{
    public class FoodDetailDto
    {
        public int Id { get; set; }

        public int? ExternalId { get; set; }

        public string Name { get; set; }

        public bool IsSystem { get; set; }

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

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public List<FoodMeasurementDto> FoodMeasurements { get; set; }
    }
}
