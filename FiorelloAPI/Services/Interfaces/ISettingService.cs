using FiorelloAPI.DTOs.Settings;
using FiorelloAPI.Models;

namespace FiorelloAPI.Services.Interfaces
{
    public interface ISettingService
    {
        Task EditAsync(Setting setting, SettingEditDto data);
        Task<Dictionary<string, string>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
    }
}
