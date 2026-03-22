using Manager.API.Dtos.Booking;
using Manager.API.Models;

namespace Manager.API.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking?> UpdateAsync(int id, UpdateBookingDto dto);
        Task<Booking?> DeleteAsync(int id);
    }
}