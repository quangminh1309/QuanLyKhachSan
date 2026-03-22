namespace Manager.API.Dtos.Booking
{
    public class UpdateBookingDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
        public string RentType { get; set; }
        public decimal TotalPrice { get; set; }
    }
}