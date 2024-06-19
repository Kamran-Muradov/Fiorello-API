using System.ComponentModel.DataAnnotations;

namespace FiorelloAPI.DTOs.Blogs
{
    public class BlogEditDto
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        public string Description { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}
