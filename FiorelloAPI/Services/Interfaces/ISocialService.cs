using FiorelloAPI.Models;

namespace FiorelloAPI.Services.Interfaces
{
    public interface ISocialService
    {
        Task<IEnumerable<Social>> GetAllAsync();
    }
}
