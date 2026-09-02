using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodMeasurementsDtos;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Dtos.FoodDtos.In
{
    public class UpdateFoodDto
    {
        [Required(ErrorMessage = "Namn på livsmedel är obligatoriskt.")]        
        [StringLength(100, ErrorMessage = "Livsmedelsnamn kan max vara 100 tecken.")]
        public string Name { get; set; }

        
        [Range(0, 100000, ErrorMessage = "Värdet måste ligga mellan 0 och 100 000.")]
        [Precision(10, 2)]
        public decimal? Oxalate { get; set; }

        [Required(ErrorMessage = "Mängd kcal (per 100 gram) är obligatoriskt.")]
        [Range(0, 1000, ErrorMessage = "Värdet måste ligga mellan 0 och 1000.")]
        [Precision(10, 2)]
        public decimal Kcal { get; set; }

        [Required(ErrorMessage = "Mängd fett (antal gram per 100 gram) är obligatoriskt.")]
        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal Fat { get; set; }

        [Required(ErrorMessage = "Mängd protein (antal gram per 100 gram) är obligatoriskt.")]
        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal Protein { get; set; }

        [Required(ErrorMessage = "Mängd kolhydrater (antal gram per 100 gram) är obligatoriskt.")]
        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal Carbohydrate { get; set; }

        [Required(ErrorMessage = "Mängd fibrer (antal gram per 100 gram) är obligatoriskt.")]
        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal Fiber { get; set; }

        [Required(ErrorMessage = "Total mängd socker (antal gram per 100 gram) är obligatoriskt.")]
        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal TotalSugar { get; set; }

        
        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal? SaturatedFat { get; set; }

        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal? MonounsaturatedFat { get; set; }

        [Range(0, 100, ErrorMessage = "Värdet måste ligga mellan 0 och 100.")]
        [Precision(10, 2)]
        public decimal? PolyunsaturatedFat { get; set; }

        [Required(ErrorMessage = "Kategori måste anges.")]
        public int CategoryId { get; set; }

        public List<FoodMeasurementDto> FoodMeasurements { get; set; }
    }
}
