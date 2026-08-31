namespace Naringskollen.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<FoodMeasurement> ApiFoodSupplements { get; set; }

        public List<Food> Foods { get; set; }
    }
}
