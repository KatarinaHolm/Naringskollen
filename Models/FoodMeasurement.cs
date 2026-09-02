using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Models
{
    public class FoodMeasurement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UnitName { get; set; }

        [Required]
        [Range(0, 1000)]
        [Precision(5, 2)]
        public decimal GramWeight { get; set; }

        [Required]
        public int FoodId { get; set; }

        public Food Food { get; set; }


    }
}
