namespace Naringskollen.Dtos.FoodSearchDtos
{
    public class SearchFoodDetailDto
    {
        public string Name { get; set; }

        public double Quantity { get; set; }

        public string Unit { get; set; }

        public double Kcal { get; set; }

        public double Fat { get; set; }

        public double Protein { get; set; }

        public double Carbohydrate { get; set; }

        public double Fiber { get; set; }

        public double TotalSugar { get; set; }

        public double? SaturatedFat { get; set; }

        public double? MonounsaturatedFat { get; set; }

        public double? PolyunsaturatedFat { get; set; }

        public double? Oxalate { get; set; }
                
        public string Category { get; set; }
    }
}
