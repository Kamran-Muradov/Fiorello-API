using System.ComponentModel.DataAnnotations;

namespace FiorelloAPI.DTOs.Sliders
{
    public class SliderCreateDto
    {
        [Required]
        public IFormFile UploadImage { get; set; }
    }
}
