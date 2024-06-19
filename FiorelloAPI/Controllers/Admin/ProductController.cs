using AutoMapper;
using FiorelloAPI.DTOs.Products;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers.Admin
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public ProductController(
            IProductService productService,
            IMapper mapper,
            ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryService.GetByIdAsync(request.CategoryId);

            if (category == null) return NotFound();

            foreach (var item in request.UploadImages)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size can be max 500 Kb");
                    return BadRequest(ModelState);
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");
                    return BadRequest();
                }
            }

            await _productService.CreateAsync(request);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ProductEditDto request)
        {
            var product = await _productService.GetByIdWithAllDatasAsync(id);

            if (product == null) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryService.GetByIdAsync(request.CategoryId);

            if (category == null) return NotFound();

            if (request.NewImages is not null)
            {
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Images", "Image size can be max 500 Kb");
                        return BadRequest(ModelState);
                    }

                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Images", "File type must be only image");
                        return BadRequest();
                    }
                }
            }

            await _productService.EditAsync(product, request);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> SetMainImage([FromBody] MainAndDeleteImageDto request)
        {
            await _productService.SetMainImageAsync(request);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();

            var product = await _productService.GetByIdWithImagesAsync((int)id);

            if (product == null) return NotFound();

            await _productService.DeleteAsync(product);

            return Ok(); 
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductImage([FromBody] MainAndDeleteImageDto request)
        {
            await _productService.DeleteProductImageAsync(request);
            
            return Ok();
        }
    }
}
