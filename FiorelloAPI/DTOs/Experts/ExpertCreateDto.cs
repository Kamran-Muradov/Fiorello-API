using System.ComponentModel.DataAnnotations;

namespace FiorelloAPI.DTOs.Experts
{
    public class ExpertCreateDto
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(100)]
        public string Position { get; set; }
        [Required]
        public IFormFile UploadImage { get; set; }
    }
}
