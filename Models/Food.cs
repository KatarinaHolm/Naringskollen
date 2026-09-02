using Microsoft.EntityFrameworkCore;
using Naringskollen.Dtos.FoodMeasurementsDtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Naringskollen.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Food
    {
        public int Id { get; set; }
                
        [JsonPropertyName("Livsmedelsnummer")]
        public int? ExternalId { get; set; }

        [Required]
        [StringLength(100)]
        [JsonPropertyName("Livsmedelsnamn")]
        public string Name { get; set; }

        [Required]
        public bool IsSystem { get; set; }

        [Range(0, 100000)]
        [Precision(10, 2)]
        [JsonPropertyName("Oxalater (mg)")]
        public decimal? Oxalate { get; set; }

        [Required]
        [Range(0, 1000)]
        [Precision(10, 2)]
        [JsonPropertyName("Energi (kcal)")]
        public decimal Kcal { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Fett, totalt (g)")]
        public decimal Fat { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Protein (g)")]
        public decimal Protein { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Kolhydrater, tillgängliga (g)")]
        public decimal Carbohydrate { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Fiber (g)")]
        public decimal Fiber { get; set; }

        [Required]
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Sockerarter, totalt (g)")]
        public decimal TotalSugar { get; set; }

        
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Summa mättade fettsyror (g)")]
        public decimal? SaturatedFat { get; set; }

        
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Summa enkelomättade fettsyror (g)")]
        public decimal? MonounsaturatedFat { get; set; }

        
        [Range(0, 100)]
        [Precision(5, 2)]
        [JsonPropertyName("Summa fleromättade fettsyror (g)")]
        public decimal? PolyunsaturatedFat { get; set; }

        [Required]        
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<FoodMeasurement> FoodMeasurements { get; set; }
    }
}
