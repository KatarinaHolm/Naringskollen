

using Naringskollen.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Naringskollen.Dtos.FoodMeasurementsDtos.Out
{
    public class FoodMeasurementSummaryDto
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FoodMeasurementUnit? Unit { get; set; }

        public decimal Grams { get; set; }
    }
}