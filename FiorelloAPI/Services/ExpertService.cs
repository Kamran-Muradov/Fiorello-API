using AutoMapper;
using FiorelloAPI.Data;
using FiorelloAPI.DTOs.Experts;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Models;
using FiorelloAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Services
{
    public class ExpertService : IExpertService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public ExpertService(
            AppDbContext context,
            IWebHostEnvironment env,
            IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task CreateAsync(ExpertCreateDto data)
        {
            string fileName = $"{Guid.NewGuid()}-{data.UploadImage.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await data.UploadImage.SaveFileToLocalAsync(path);

            Expert expert = _mapper.Map<Expert>(data);

            expert.Image = fileName;

            await _context.Experts.AddAsync(expert);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Expert expert, ExpertEditDto data)
        {
            if (data.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", expert.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{data.NewImage.FileName}";

                string newPath = _env.GenerateFilePath("img", fileName);

                await data.NewImage.SaveFileToLocalAsync(newPath);

                expert.Image = fileName;
            }

            _mapper.Map(data, expert);
            _context.Experts.Update(expert);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expert expert)
        {
            string imagePath = _env.GenerateFilePath("img", expert.Image);
            imagePath.DeleteFileFromLocal();

            _context.Experts.Remove(expert);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expert>> GetAllAsync()
        {
            return await _context.Experts
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<Expert> GetByIdAsync(int id)
        {
            return await _context.Experts
                .Where(m => m.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
