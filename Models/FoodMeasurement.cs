using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Naringskollen.Models
{
    public class FoodMeasurement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [JsonPropertyName("Enhet")]
        public string UnitName { get; set; }

        [Required]
        [Range(0, 1000)]
        [Precision(6, 2)]
        [JsonPropertyName("Gram")]
        public decimal GramWeight { get; set; }

        [Required]
        public int FoodId { get; set; }

        public Food Food { get; set; }


    }
}
