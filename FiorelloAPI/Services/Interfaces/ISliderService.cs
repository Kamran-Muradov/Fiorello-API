using FiorelloAPI.DTOs.Sliders;
using FiorelloAPI.Models;

namespace FiorelloAPI.Services.Interfaces
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateDto data);
        Task EditAsync(Slider slider, SliderEditDto data);
        Task DeleteAsync(Slider slider);
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<SliderInfo> GetSliderInfoAsync();
    }
}
