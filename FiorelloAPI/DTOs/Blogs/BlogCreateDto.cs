using System.ComponentModel.DataAnnotations;

namespace FiorelloAPI.DTOs.Blogs
{
    public class BlogCreateDto
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        public string Description { get; set; }
        [Required]
        public IFormFile UploadImage { get; set; }
    }
}
