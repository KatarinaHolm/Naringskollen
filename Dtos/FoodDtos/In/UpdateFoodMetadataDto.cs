using Naringskollen.Dtos.FoodMeasurementsDtos;

namespace Naringskollen.Dtos.FoodDtos.In
{
    public class UpdateFoodMetadataDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Oxalate { get; set; }

        public int CategoryId { get; set; }

        public List<FoodMeasurementDto> FoodMeasurements { get; set; }
    }
}
