using System.ComponentModel.DataAnnotations;

namespace FiorelloAPI.DTOs.Settings
{
    public class SettingEditDto
    {
        [Required]
        public string Value { get; set; }
    }
}
