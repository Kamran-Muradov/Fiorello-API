using FiorelloAPI.DTOs.Products;
using FiorelloAPI.Models;

namespace FiorelloAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateDto data);
        Task EditAsync(Product product, ProductEditDto data);
        Task DeleteAsync(Product product);
        Task<IEnumerable<Product>> GetAllWithImagesAsync();
        Task<Product> GetByIdWithAllDatasAsync(int id);
        Task<Product> GetByIdWithImagesAsync(int id);
        Task DeleteProductImageAsync(MainAndDeleteImageDto data);
        Task SetMainImageAsync(MainAndDeleteImageDto data);

    }
}
