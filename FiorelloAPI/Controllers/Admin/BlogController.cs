using AutoMapper;
using FiorelloAPI.DTOs.Blogs;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Models;
using FiorelloAPI.Services;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers.Admin
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _blogService.ExistAsync(request.Title))
            {
                ModelState.AddModelError("Title", "This blog already exist");
                return BadRequest(ModelState);
            }

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

            await _blogService.CreateAsync(request);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] BlogEditDto request)
        {
            var blog = await _blogService.GetByIdAsync(id);

            if (blog is null) return NotFound();

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

            if (blog.Title.Trim().ToLower() != request.Title.Trim().ToLower() && await _blogService.ExistAsync(request.Title))
            {
                ModelState.AddModelError("Title", "This blog already exist");
                return BadRequest(ModelState);
            }

            await _blogService.EditAsync(blog, request);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();

            var blog = await _blogService.GetByIdAsync(id);

            if (blog is null) return NotFound();

            await _blogService.DeleteAsync(blog);
            return Ok();
        }
    }
}
