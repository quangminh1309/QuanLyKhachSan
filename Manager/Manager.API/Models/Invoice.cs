namespace Manager.API.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int BookingId { get; set; }        // FK → Booking (1-1)
        public decimal RoomCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }        // "Unpaid", "Paid", "Refunded"
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        // Navigation
        public Booking Booking { get; set; }
        public ICollection<InvoiceService> InvoiceServices { get; set; }
    }
}