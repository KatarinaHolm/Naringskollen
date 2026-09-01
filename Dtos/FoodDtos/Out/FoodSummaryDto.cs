using Naringskollen.Dtos.FoodMeasurementsDtos;
using Naringskollen.Models;

namespace Naringskollen.Dtos.FoodDtos.Out
{
    public class FoodSummaryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsSystem { get; set; }      
        
        public string Category { get; set; }

        public List<FoodMeasurementDto> FoodMeasurements { get; set; }
    }
}
