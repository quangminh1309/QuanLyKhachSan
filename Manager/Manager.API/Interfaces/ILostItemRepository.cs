using Manager.API.Dtos.LostItem;
using Manager.API.Models;

namespace Manager.API.Interfaces
{
    public interface ILostItemRepository
    {
        Task<List<LostItem>> GetAllAsync();
        Task<LostItem?> GetByIdAsync(int id);
        Task<LostItem> CreateAsync(LostItem lostItem);
        Task<LostItem?> UpdateAsync(int id, UpdateLostItemDto dto);
        Task<LostItem?> DeleteAsync(int id);
    }
}