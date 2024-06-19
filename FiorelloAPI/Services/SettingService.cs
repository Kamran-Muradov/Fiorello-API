using AutoMapper;
using FiorelloAPI.Data;
using FiorelloAPI.DTOs.Settings;
using FiorelloAPI.Models;
using FiorelloAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SettingService(
            AppDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task EditAsync(Setting setting, SettingEditDto data)
        {
            _mapper.Map(data, setting);

            _context.Settings.Update(setting);

            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings
                .AsNoTracking()
                .ToDictionaryAsync(m => m.Key, m => m.Value);
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
