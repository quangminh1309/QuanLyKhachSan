using Manager.API.Dtos.Incident;
using Manager.API.Models;

namespace Manager.API.Interfaces
{
    public interface IIncidentRepository
    {
        Task<List<Incident>> GetAllAsync();
        Task<Incident?> GetByIdAsync(int id);
        Task<Incident> CreateAsync(Incident incident);
        Task<Incident?> UpdateAsync(int id, UpdateIncidentDto dto);
        Task<Incident?> DeleteAsync(int id);
        Task<List<Incident>> GetByUsernameAsync(string username);
    }
}