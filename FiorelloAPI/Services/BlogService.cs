using AutoMapper;
using FiorelloAPI.Data;
using FiorelloAPI.DTOs.Blogs;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Models;
using FiorelloAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public BlogService(
            AppDbContext context,
            IWebHostEnvironment env,
            IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task CreateAsync(BlogCreateDto data)
        {
            string fileName = $"{Guid.NewGuid()}-{data.UploadImage.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await data.UploadImage.SaveFileToLocalAsync(path);

            Blog blog = _mapper.Map<Blog>(data);

            blog.Image = fileName;

            await _context.Blogs.AddAsync(blog);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Blog blog, BlogEditDto data)
        {
            if (data.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", blog.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{data.NewImage.FileName}";

                string newPath = _env.GenerateFilePath("img", fileName);

                await data.NewImage.SaveFileToLocalAsync(newPath);

                blog.Image = fileName;
            }

            _mapper.Map(data, blog);
            _context.Blogs.Update(blog);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
            string imagePath = _env.GenerateFilePath("img", blog.Image);
            imagePath.DeleteFileFromLocal();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Where(m => m.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistAsync(string title)
        {
            return await _context.Blogs.AnyAsync(m => m.Title.Trim() == title.Trim());
        }
    }
}
