using AutoMapper;
using FiorelloAPI.Data;
using FiorelloAPI.DTOs.Sliders;
using FiorelloAPI.Helpers.Extensions;
using FiorelloAPI.Models;
using FiorelloAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public SliderService(
            AppDbContext context,
            IWebHostEnvironment env, 
            IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task CreateAsync(SliderCreateDto data)
        {
            string fileName = $"{Guid.NewGuid()}-{data.UploadImage.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await data.UploadImage.SaveFileToLocalAsync(path);

            Slider slider = _mapper.Map<Slider>(data);

            slider.Image = fileName;

            await _context.AddAsync(slider);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Slider slider, SliderEditDto data)
        {
            if (data.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", slider.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{data.NewImage.FileName}";

                string newPath = _env.GenerateFilePath("img", fileName);

                await data.NewImage.SaveFileToLocalAsync(newPath);

                slider.Image = fileName;
            }

            _mapper.Map(data, slider);
            _context.Sliders.Update(slider);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Slider slider)
        {
            string imagePath = _env.GenerateFilePath("img", slider.Image);
            imagePath.DeleteFileFromLocal();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<SliderInfo> GetSliderInfoAsync()
        {
            return await _context.SliderInfos
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
