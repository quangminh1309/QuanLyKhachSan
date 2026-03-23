using System.ComponentModel.DataAnnotations;

namespace Manager.API.Dtos.Booking
{
    public class CreateBookingRequestDto
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
    }
}
