using System.ComponentModel.DataAnnotations;

namespace FiorelloAPI.DTOs.Categories
{
    public class CategoryEditDto
    {
        [Required]
        public string Name { get; set; }
    }
}
