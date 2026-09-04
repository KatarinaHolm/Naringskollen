using Naringskollen.Dtos.FoodMeasurementsDtos.Out;

namespace Naringskollen.Dtos.FoodDtos.Out
{
    public class FoodSummaryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSystem { get; set; }      
        
        public string Category { get; set; }

        public List<FoodMeasurementSummaryDto> FoodMeasurements { get; set; }
    }
}
