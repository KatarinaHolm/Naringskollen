using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }        

        public List<Food> Foods { get; set; }
    }
}
