using FiorelloAPI.DTOs.Experts;
using FiorelloAPI.Models;

namespace FiorelloAPI.Services.Interfaces
{
    public interface IExpertService
    {
        Task CreateAsync(ExpertCreateDto data);
        Task EditAsync(Expert expert, ExpertEditDto data);
        Task DeleteAsync(Expert expert);
        Task<IEnumerable<Expert>> GetAllAsync();
        Task<Expert> GetByIdAsync(int id);
    }
}
