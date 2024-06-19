using System.ComponentModel.DataAnnotations;

namespace FiorelloAPI.DTOs.Categories
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
