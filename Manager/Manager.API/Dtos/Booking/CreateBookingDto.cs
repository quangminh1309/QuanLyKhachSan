namespace Manager.API.Dtos.Booking
{
    public class CreateBookingDto
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RentType { get; set; }
        public decimal TotalPrice { get; set; }
    }
}