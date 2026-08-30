namespace Naringskollen.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<ApiFoodSupplement> ApiFoodSupplements { get; set; }

        public List<SystemFood> SystemFoods { get; set; }
    }
}
