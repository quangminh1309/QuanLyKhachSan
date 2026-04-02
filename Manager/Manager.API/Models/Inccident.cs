namespace Manager.API.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public int BookingId { get; set; }        // FK → Booking
        public string UserId { get; set; }        // FK → AspNetUsers
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }        // "Pending", "Resolved", "Cancelled"
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        // Navigation
        public Booking Booking { get; set; } = null!;
        public AppUser User { get; set; } = null!;
    }
}