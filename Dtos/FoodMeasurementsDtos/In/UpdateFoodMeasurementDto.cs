using Microsoft.EntityFrameworkCore;
using Naringskollen.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Dtos.FoodMeasurementsDtos.In
{
    public class UpdateFoodMeasurementDto
    {
        [Required(ErrorMessage = "Id på enhet är obligatoriskt")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Namn på enhet är obligatoriskt")]
        public FoodMeasurementUnit? Unit { get; set; }

        [Required(ErrorMessage = "Antal gram för vald enhet är obligatoriskt.")]
        [Range(0, 1000, ErrorMessage = "Värdet måste ligga mellan 0 och 1000.")]
        [Precision(5, 2)]
        public decimal? Grams { get; set; }
    }
}
