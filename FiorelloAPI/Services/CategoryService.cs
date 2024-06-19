using AutoMapper;
using FiorelloAPI.Data;
using FiorelloAPI.DTOs.Categories;
using FiorelloAPI.Models;
using FiorelloAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CategoryCreateDto data)
        {
            await _context.Categories.AddAsync(_mapper.Map<Category>(data));
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Category category, CategoryEditDto data)
        {
            _mapper.Map(data, category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task SetToArchiveAsync(Category category)
        {
            category.SoftDeleted = true;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task RestoreAsync(Category category)
        {
            category.SoftDeleted = false;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllArchivedAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(m => m.SoftDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
        {
            return await _context.Categories
                .Include(m => m.Products)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Category> GetByIdWithProductsAsync(int id)
        {
            return await _context.Categories
                .Where(m => m.Id == id)
                .Include(m => m.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }
    }
}
