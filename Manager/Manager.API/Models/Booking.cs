namespace Manager.API.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int NumberOfGuests { get; set; } // từ Minh

        public string Status { get; set; }

        public string RentType { get; set; } // từ MaiLan

        public decimal TotalPrice { get; set; } //  (bỏ TotalAmount)

        public string? SpecialRequests { get; set; } // từ Minh

        public bool RefundRequested { get; set; } = false;
        public string? RefundReason { get; set; }
        public DateTime? RefundRequestedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        public AppUser User { get; set; } = null!;
        public Rooms Room { get; set; } = null!;

        public Invoice? Invoice { get; set; } // MaiLan - nullable vì có thể chưa có invoice
        public ICollection<LostItem> LostItems { get; set; } = new List<LostItem>(); // MaiLan
        public ICollection<Review> Reviews { get; set; } = new List<Review>(); // MaiLan
        public ICollection<Incident> Incidents { get; set; } = new List<Incident>(); // MaiLan

        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>(); // Minh
        public ICollection<Payment> Payments { get; set; } = new List<Payment>(); // Minh
    }
}