using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodMeasurementsDtos;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Models
{
    public class Food
    {
        public int Id { get; set; }

        public int? ExternalId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsSystem { get; set; }

        [Range(0, 100000)]
        [Precision(10, 2)]
        public decimal? Oxalate { get; set; }

        [Required]
        [Range(0, 1000)]
        [Precision(10, 2)]
        public decimal Kcal { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal Fat { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal Protein { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal Carbohydrate { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal Fiber { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal TotalSugar { get; set; }

        
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal? SaturatedFat { get; set; }

        
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal? MonounsaturatedFat { get; set; }

        
        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal? PolyunsaturatedFat { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<FoodMeasurement> FoodMeasurements { get; set; }
    }
}
