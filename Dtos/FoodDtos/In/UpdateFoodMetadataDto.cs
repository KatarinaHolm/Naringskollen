using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodMeasurementsDtos;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Dtos.FoodDtos.In
{
    public class UpdateFoodMetadataDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namn på livsmedel är obligatoriskt.")]
        [StringLength(100, ErrorMessage = "Livsmedelsnamn kan max vara 100 tecken.")]
        public string Name { get; set; }

        [Range(0, 100000, ErrorMessage = "Värdet måste ligga mellan 0 och 100 000.")]
        [Precision(10, 2)]
        public decimal? Oxalate { get; set; }

        [Required(ErrorMessage = "Kategori måste anges.")]      
        public int CategoryId { get; set; }

        public List<FoodMeasurementDto> FoodMeasurements { get; set; }
    }
}
