using AutoMapper;
using FiorelloAPI.DTOs.Categories;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers.Admin
{
    public class ArchiveController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ArchiveController(
            ICategoryService categoryService, 
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryArchive()
        {
            return Ok(_mapper.Map<IEnumerable<CategoryArchiveDto>>(await _categoryService.GetAllArchivedAsync()));
        }

        [HttpPut]
        public async Task<IActionResult> RestoreCategory([FromQuery] int? id)
        {
            if (id == null) return BadRequest(ModelState);

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            await _categoryService.RestoreAsync(category);

            return Ok();
        }
    }
}
