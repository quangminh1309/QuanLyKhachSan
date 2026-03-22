using Manager.API.Data;
using Manager.API.Dtos.LostItem;
using Manager.API.Interfaces;
using Manager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.API.Repository
{
    public class LostItemRepository : ILostItemRepository
    {
        private readonly ApplicationDBContext _context;
        public LostItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LostItem>> GetAllAsync()
        {
            return await _context.LostItems.ToListAsync();
        }

        public async Task<LostItem?> GetByIdAsync(int id)
        {
            return await _context.LostItems.FindAsync(id);
        }

        public async Task<LostItem> CreateAsync(LostItem lostItem)
        {
            lostItem.CreateAt = DateTime.Now;
            lostItem.UpdateAt = DateTime.Now;
            lostItem.Status = "Lost";
            await _context.LostItems.AddAsync(lostItem);
            await _context.SaveChangesAsync();
            return lostItem;
        }

        public async Task<LostItem?> UpdateAsync(int id, UpdateLostItemDto dto)
        {
            var lostItem = await _context.LostItems.FindAsync(id);
            if (lostItem == null) return null;

            lostItem.ItemName = dto.ItemName;
            lostItem.Description = dto.Description;
            lostItem.Status = dto.Status;
            lostItem.FoundDate = dto.FoundDate;
            lostItem.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return lostItem;
        }

        public async Task<LostItem?> DeleteAsync(int id)
        {
            var lostItem = await _context.LostItems.FindAsync(id);
            if (lostItem == null) return null;

            _context.LostItems.Remove(lostItem);
            await _context.SaveChangesAsync();
            return lostItem;
        }
    }
}