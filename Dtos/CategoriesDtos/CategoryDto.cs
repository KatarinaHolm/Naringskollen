using System.ComponentModel.DataAnnotations;

namespace Naringskollen.Dtos.CategoriesDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namn på kategori är obligatoriskt.")]
        [StringLength(100, ErrorMessage = "Namn på kategori kan max vara 100 tecken.")]
        public string Name { get; set; }
    }
}
