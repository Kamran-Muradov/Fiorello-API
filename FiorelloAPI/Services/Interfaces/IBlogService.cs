using FiorelloAPI.DTOs.Blogs;
using FiorelloAPI.Models;

namespace FiorelloAPI.Services.Interfaces
{
    public interface IBlogService
    {
        Task CreateAsync(BlogCreateDto data);
        Task EditAsync(Blog blog, BlogEditDto data);
        Task DeleteAsync(Blog blog);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<bool> ExistAsync(string title);
    }
}
