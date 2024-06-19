using AutoMapper;
using FiorelloAPI.DTOs;
using FiorelloAPI.DTOs.Sliders;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers
{
    public class SliderController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISliderService _sliderService;

        public SliderController(
            IMapper mapper,
            ISliderService sliderService)
        {
            _mapper = mapper;
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sliders = _mapper.Map<List<SliderDto>>(await _sliderService.GetAllAsync());
            var sliderInfo = _mapper.Map<SliderInfoDto>(await _sliderService.GetSliderInfoAsync());



            return Ok(new SliderWithInfoDto
            {
                Sliders = sliders,
                SliderInfo = sliderInfo
            });

        }
    }
}
