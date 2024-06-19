using AutoMapper;
using FiorelloAPI.DTOs.Categories;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(await _categoryService.GetAllAsync()));
        }
    }
}
