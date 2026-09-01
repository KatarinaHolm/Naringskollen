namespace Naringskollen.Models
{
    public class FoodMeasurement
    {
        public int Id { get; set; }        

        public string UnitName { get; set; }

        public double GramWeight { get; set; }

        public int FoodId { get; set; }

        public Food Food { get; set; }


    }
}
