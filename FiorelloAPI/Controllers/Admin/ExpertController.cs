using AutoMapper;
using FiorelloAPI.DTOs.Experts;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers.Admin
{
    public class ExpertController : BaseController
    {
        private readonly IExpertService _expertService;
        private readonly IMapper _mapper;

        public ExpertController(
            IExpertService expertService,
            IMapper mapper)
        {
            _expertService = expertService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var expert = await _expertService.GetByIdAsync(id);

            if (expert == null) return NotFound();

            return Ok(_mapper.Map<ExpertDetailDto>(expert));
        }

         [HttpPost]
        public async Task<IActionResult> Create([FromForm] ExpertCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!request.UploadImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("UploadImage", "Input can accept only image format");
                return BadRequest(ModelState);
            }

            if (!request.UploadImage.CheckFileSize(500))
            {
                ModelState.AddModelError("UploadImage", "Image size must be max 500 KB");
                return BadRequest(ModelState);
            }

            await _expertService.CreateAsync(request);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ExpertEditDto request)
        {
            var expert = await _expertService.GetByIdAsync(id);

            if (expert is null) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    return BadRequest(ModelState);
                }

                if (!request.NewImage.CheckFileSize(500))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 500 KB");
                    return BadRequest(ModelState);
                }
            }

            await _expertService.EditAsync(expert, request);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();

            var expert = await _expertService.GetByIdAsync(id);

            if (expert is null) return NotFound();

            await _expertService.DeleteAsync(expert);
            return Ok();
        }
    }
}
