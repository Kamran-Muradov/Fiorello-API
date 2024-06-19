using AutoMapper;
using FiorelloAPI.DTOs.Blogs;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(
            IBlogService blogService,
            IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<BlogDto>>(await _blogService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var blog = await _blogService.GetByIdAsync(id);

            if (blog == null) return NotFound();

            return Ok(_mapper.Map<BlogDetailDto>(blog));
        }
    }
}
