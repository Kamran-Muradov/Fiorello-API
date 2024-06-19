using AutoMapper;
using FiorelloAPI.DTOs.Sliders;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers.Admin
{
    public class SliderController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;

        public SliderController(
            IMapper mapper,
            ISliderService sliderService,
            IWebHostEnvironment env)
        {
            _mapper = mapper;
            _sliderService = sliderService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (!request.UploadImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Input can accept only image format");
                return BadRequest(ModelState);
            }

            if (!request.UploadImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                return BadRequest(ModelState);
            }

            await _sliderService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] SliderEditDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var slider = await _sliderService.GetByIdAsync(id);

            if (slider is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    return BadRequest(ModelState);
                }

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                    return BadRequest(ModelState);
                }
            }

            await _sliderService.EditAsync(slider, request);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            await _sliderService.DeleteAsync(slider);

            return Ok();
        }
    }
}
