namespace Manager.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }        // FK → AspNetUsers (Identity)
        public int RoomId { get; set; }           // FK → Rooms
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }        // "Pending", "Confirmed", "CheckedIn", "CheckedOut", "Cancelled"
        public string RentType { get; set; }      // "Daily", "Monthly", "Hourly"
        public decimal TotalPrice { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        // Navigation
        public AppUser User { get; set; }
        public Rooms Room { get; set; }
        public Invoice Invoice { get; set; }
        public ICollection<LostItem> LostItems { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Incident> Incidents { get; set; }
    }
}