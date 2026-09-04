using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Dtos.FoodMeasurementsDtos.Out
{
    public class FoodMeasurementSummaryDto
    {       
        public string Unit { get; set; }
           
        public decimal Grams { get; set; }
    }
}
}
