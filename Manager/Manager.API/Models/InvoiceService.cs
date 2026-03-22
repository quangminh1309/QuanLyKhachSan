namespace Manager.API.Models
{
    public class InvoiceService
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }        // FK → Invoice
        public int ServiceId { get; set; }        // FK → Services
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;

        // Navigation
        public Invoice Invoice { get; set; }
        public Services Service { get; set; }
    }
}