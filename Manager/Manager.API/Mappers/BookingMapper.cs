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
                RoomNumber = booking.Room?.RoomNumber ?? "",
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfGuests = booking.NumberOfGuests,
                Status = booking.Status,
                RentType = booking.RentType,
                TotalPrice = booking.TotalPrice,
                SpecialRequests = booking.SpecialRequests,
                CreatedAt = booking.CreatedAt
            };
        }

        // Dùng cho nhánh HEAD (CreateBookingDto cũ - có UserId và TotalPrice truyền vào)
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

        // Dùng cho nhánh Minh (CreateBookingRequestDto - userId lấy từ token)
        public static Booking ToBookingFromCreate(this CreateBookingRequestDto dto, string userId)
        {
            return new Booking
            {
                UserId = userId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                NumberOfGuests = dto.NumberOfGuests,
                Status = "Pending",
                SpecialRequests = dto.SpecialRequests,
                TotalPrice = 0
            };
        }
    }
}