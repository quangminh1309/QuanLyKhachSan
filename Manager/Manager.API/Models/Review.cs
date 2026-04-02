namespace Manager.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BookingId { get; set; }        // FK → Booking
        public string UserId { get; set; }        // FK → AspNetUsers
        public int Rating { get; set; }           // 1-5
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        // Navigation
        public Booking Booking { get; set; } = null!;
        public AppUser User { get; set; } = null!;
    }
}