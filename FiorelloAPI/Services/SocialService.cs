using FiorelloAPI.Data;
using FiorelloAPI.Models;
using FiorelloAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloAPI.Services
{
    public class SocialService : ISocialService
    {
        private readonly AppDbContext _context;

        public SocialService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Social>> GetAllAsync()
        {
            return await _context.Socials
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
