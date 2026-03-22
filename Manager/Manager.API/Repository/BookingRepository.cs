using Manager.API.Data;
using Manager.API.Dtos.Booking;
using Manager.API.Interfaces;
using Manager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.API.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDBContext _context;
        public BookingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            booking.CreateAt = DateTime.Now;
            booking.UpdateAt = DateTime.Now;
            booking.Status = "Pending";
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> UpdateAsync(int id, UpdateBookingDto dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;

            booking.CheckInDate = dto.CheckInDate;
            booking.CheckOutDate = dto.CheckOutDate;
            booking.Status = dto.Status;
            booking.RentType = dto.RentType;
            booking.TotalPrice = dto.TotalPrice;
            booking.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return null;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}