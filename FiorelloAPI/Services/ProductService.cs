using AutoMapper;
using FiorelloAPI.Data;
using FiorelloAPI.DTOs.Products;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Models;
using FiorelloAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public ProductService(
            AppDbContext context,
            IWebHostEnvironment env,
            IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProductCreateDto data)
        {
            List<ProductImage> images = new();

            foreach (var item in data.UploadImages)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                string path = _env.GenerateFilePath("img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new ProductImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Product product = _mapper.Map<Product>(data);

            product.ProductImages = images;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(Product product, ProductEditDto data)
        {
            if (data.NewImages is not null)
            {
                foreach (var item in data.NewImages)
                {
                    string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                    string path = _env.GenerateFilePath("img", fileName);

                    await item.SaveFileToLocalAsync(path);

                    product.ProductImages.Add(new ProductImage { Name = fileName });
                }
            }

            _mapper.Map(data, product);

            _context.Products.Update(product);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            foreach (var item in product.ProductImages)
            {
                string path = _env.GenerateFilePath("img", item.Name);
                path.DeleteFileFromLocal();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithImagesAsync()
        {
            return await _context.Products
                .Include(m => m.ProductImages)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetByIdWithAllDatasAsync(int id)
        {
            return await _context.Products
                .Where(m => m.Id == id)
                .Include(m => m.Category)
                .Include(m => m.ProductImages)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetByIdWithImagesAsync(int id)
        {
            return await _context.Products
                .Where(m => m.Id == id)
                .Include(m => m.ProductImages)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task DeleteProductImageAsync(MainAndDeleteImageDto data)
        {
            var product = await _context.Products
                .Where(m => m.Id == data.ProductId)
                .Include(m => m.ProductImages)
                .FirstOrDefaultAsync();

            var productImage = product.ProductImages.FirstOrDefault(m => m.Id == data.ImageId);

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();

            string path = _env.GenerateFilePath("img", productImage.Name);
            path.DeleteFileFromLocal();
        }

        public async Task SetMainImageAsync(MainAndDeleteImageDto data)
        {
            var product = await _context.Products
                .Where(m => m.Id == data.ProductId)
                .Include(m => m.ProductImages)
                .FirstOrDefaultAsync();

            var productImage = product.ProductImages.FirstOrDefault(m => m.Id == data.ImageId);

            product.ProductImages.FirstOrDefault(m => m.IsMain).IsMain = false;
            productImage.IsMain = true;
            await _context.SaveChangesAsync();
        }
    }
}
