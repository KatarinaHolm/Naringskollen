using Microsoft.EntityFrameworkCore;
using Naringskollen.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Naringskollen.Models
{
    public class FoodMeasurement
    {
        public int Id { get; set; }

        [Required]        
        [JsonPropertyName("Enhet")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FoodMeasurementUnit Unit { get; set; }

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
