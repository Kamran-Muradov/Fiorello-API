using AutoMapper;
using FiorelloAPI.DTOs.Categories;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers.Admin
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _categoryService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return BadRequest(ModelState);
            }

            await _categoryService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CategoryProductDto>>(await _categoryService.GetAllWithProductsAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var category = await _categoryService.GetByIdWithProductsAsync(id);

            if (category is null) return NotFound();

            return Ok(_mapper.Map<CategoryDetailDTO>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CategoryEditDto request)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (category.Name.Trim().ToLower() != request.Name.Trim().ToLower() && await _categoryService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return BadRequest(ModelState);
            }

            await _categoryService.EditAsync(category, request);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest(ModelState);

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            await _categoryService.DeleteAsync(category);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> SetToArchive([FromQuery] int? id)
        {
            if (id == null) return BadRequest(ModelState);

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            await _categoryService.SetToArchiveAsync(category);

            return Ok();
        }
    }
}
