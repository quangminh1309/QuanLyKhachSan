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

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation
        public AppUser User { get; set; }
        public Rooms Room { get; set; }

        public Invoice Invoice { get; set; } // MaiLan
        public ICollection<LostItem> LostItems { get; set; } // MaiLan
        public ICollection<Review> Reviews { get; set; } // MaiLan
        public ICollection<Incident> Incidents { get; set; } // MaiLan

        public ICollection<BookingService> BookingServices { get; set; } // Minh
        public ICollection<Payment> Payments { get; set; } // Minh
    }
}