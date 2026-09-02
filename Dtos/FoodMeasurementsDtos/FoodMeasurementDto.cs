using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Dtos.FoodMeasurementsDtos
{
    public class FoodMeasurementDto
    {
        [Required(ErrorMessage = "Namn på enhet är obligatoriskt")]
        [StringLength(100, ErrorMessage = "Enhetsnamn kan max vara 100 tecken.")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Antal gram för vald enhet är obligatoriskt.")]
        [Range(0, 1000, ErrorMessage = "Värdet måste ligga mellan 0 och 1000.")]
        [Precision(5, 2)]
        public decimal Grams { get; set; }
    }
}
