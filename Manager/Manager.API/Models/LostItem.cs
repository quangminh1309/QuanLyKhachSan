namespace Manager.API.Models
{
    public class LostItem
    {
        public int Id { get; set; }
        public int BookingId { get; set; }        // FK → Booking
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }        // "Lost", "Found", "Returned"
        public DateTime FoundDate { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        // Navigation
        public Booking Booking { get; set; } = null!;
    }
}