using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodMeasurementsDtos.In;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Dtos.FoodDtos.In
{
    public class UpdateFoodMetadataDto
    {       
        [Range(0, 100000, ErrorMessage = "Värdet måste ligga mellan 0 och 100 000.")]
        [Precision(10, 2)]
        public decimal? Oxalate { get; set; }

        [Required(ErrorMessage = "Kategori måste anges.")]      
        public int CategoryId { get; set; }

        public List<UpdateFoodMeasurementDto> FoodMeasurements { get; set; } = [];
    }
}
