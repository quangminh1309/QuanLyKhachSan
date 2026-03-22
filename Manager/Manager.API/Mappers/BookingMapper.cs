using Manager.API.Dtos.Booking;
using Manager.API.Models;

namespace Manager.API.Mappers
{
    public static class BookingMapper
    {
        public static BookingDto ToBookingDto(this Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                Status = booking.Status,
                RentType = booking.RentType,
                TotalPrice = booking.TotalPrice,
                CreateAt = booking.CreateAt
            };
        }

        public static Booking ToBooking(this CreateBookingDto dto)
        {
            return new Booking
            {
                UserId = dto.UserId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                RentType = dto.RentType,
                TotalPrice = dto.TotalPrice
            };
        }
    }
}