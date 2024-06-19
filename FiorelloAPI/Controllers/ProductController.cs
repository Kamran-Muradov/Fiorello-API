using AutoMapper;
using FiorelloAPI.DTOs.Products;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(
            IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ProductMainImageDto>>(await _productService.GetAllWithImagesAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var product = await _productService.GetByIdWithAllDatasAsync(id);

            if (product is null) return NotFound();

            return Ok(_mapper.Map<ProductDetailDto>(product));
        }
    }
}
